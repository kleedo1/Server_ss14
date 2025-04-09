using Content.Server.Administration.Logs;
using Content.Server.Antag;
using Content.Server.EUI;
using Content.Server.Flash;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.Mind;
using Content.Server.Popups;
using Content.Server.Revolutionary;
using Content.Server.Revolutionary.Components;
using Content.Server.Roles;
using Content.Server.RoundEnd;
using Content.Server.Shuttles.Systems;
using Content.Server.Station.Systems;
using Content.Shared.Database;
using Content.Shared.GameTicking.Components;
using Content.Shared.Humanoid;
using Content.Shared.IdentityManagement;
using Content.Shared.Mind.Components;
using Content.Shared.Mindshield.Components;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.Mobs.Systems;
using Content.Shared.NPC.Prototypes;
using Content.Shared.NPC.Systems;
using Content.Shared.Revolutionary.Components;
using Content.Shared.Stunnable;
using Content.Shared.Zombies;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using Content.Shared.Cuffs.Components;
using Content.Shared.Revolutionary;
using Robust.Server.Player;
using Content.Server.Actions;
using Robust.Shared.Player;
using Content.Server.Station.Components;
using Content.Server.AlertLevel;
using System.Linq;
using Content.Shared.NPC.Components;
using Content.Server.Chat.Systems;
using Content.Shared.Mind;

namespace Content.Server.GameTicking.Rules;

/// <summary>
/// Where all the main stuff for Revolutionaries happens (Assigning Head Revs, Command on station, and checking for the game to end.)
/// </summary>
public sealed class RevolutionaryRuleSystem : GameRuleSystem<RevolutionaryRuleComponent>
{
    [Dependency] private readonly IAdminLogManager _adminLogManager = default!;
    [Dependency] private readonly AntagSelectionSystem _antag = default!;
    [Dependency] private readonly EmergencyShuttleSystem _emergencyShuttle = default!;
    [Dependency] private readonly EuiManager _euiMan = default!;
    [Dependency] private readonly MindSystem _mind = default!;
    [Dependency] private readonly MobStateSystem _mobState = default!;
    [Dependency] private readonly NpcFactionSystem _npcFaction = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly RoleSystem _role = default!;
    [Dependency] private readonly SharedStunSystem _stun = default!;
    [Dependency] private readonly RoundEndSystem _roundEnd = default!;
    [Dependency] private readonly StationSystem _stationSystem = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly AlertLevelSystem _alertLevel = default!;
    [Dependency] private readonly ChatSystem _chatSystem = default!;

    //Used in OnPostFlash, no reference to the rule component is available
    public readonly ProtoId<NpcFactionPrototype> RevolutionaryNpcFaction = "Revolutionary";
    public readonly ProtoId<NpcFactionPrototype> RevPrototypeId = "Rev";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CommandStaffComponent, MobStateChangedEvent>(OnCommandMobStateChanged);

        SubscribeLocalEvent<HeadRevolutionaryComponent, HeadRevConvertActionEvent>(OnTargetWithConvertWindow);
        SubscribeLocalEvent<HeadRevolutionaryComponent, AfterFlashedEvent>(OnPostFlash);
        SubscribeLocalEvent<HeadRevolutionaryComponent, MobStateChangedEvent>(OnHeadRevMobStateChanged);

        SubscribeLocalEvent<RevolutionaryRoleComponent, GetBriefingEvent>(OnGetBriefing);

        SubscribeLocalEvent<HeadRevolutionaryComponent, MapInitEvent>(OnPendingMapInit);
    }

    protected override void Started(EntityUid uid, RevolutionaryRuleComponent component, GameRuleComponent gameRule, GameRuleStartedEvent args)
    {
        base.Started(uid, component, gameRule, args);
        component.Check = _timing.CurTime + component.TimerWait;
    }

    protected override void ActiveTick(EntityUid uid, RevolutionaryRuleComponent component, GameRuleComponent gameRule, float frameTime)
    {
        base.ActiveTick(uid, component, gameRule, frameTime);

        // This mess ahead needs a fix

        if (component.Check <= _timing.CurTime)
        {
            component.Check = _timing.CurTime + component.TimerWait;

            if (component.Stage == RevolutionaryStage.Initial && (GetRevsFraction() >= 0.35f || CheckCommandLose()))
            {
                component.Stage = RevolutionaryStage.Massacre;

                var stations = new HashSet<EntityUid>(); // strange

                foreach (var station in _stationSystem.GetStationsSet())
                {
                    if (TryComp<StationDataComponent>(station, out _) && TryComp<NpcFactionMemberComponent>(station, out _)
                    && _npcFaction.IsMember(station, "NanoTrasen"))
                        stations.Add(station);
                }

                var stationUid = stations.First(); // why

                if (_alertLevel.GetLevel(stationUid) == "green" || _alertLevel.GetLevel(stationUid) == "blue" || _alertLevel.GetLevel(stationUid) == "violet" || _alertLevel.GetLevel(stationUid) == "yellow")
                    _alertLevel.SetLevel(stationUid, "red", false, true, false, false);

                var sessionData = _antag.GetAntagIdentifiers(uid);
                string headRevsNames = "";
                foreach (var (mind, data, name) in sessionData) // cursed
                {
                    if (!_role.MindHasRole<RevolutionaryRoleComponent>(mind, out var role)) 
                        continue;
                    headRevsNames += name + ", ";

                    if (!TryComp<MindComponent>(mind, out var mindComponent))
                        continue;

                    if (mindComponent.OwnedEntity == null)
                        continue;

                    RaiseLocalEvent(mindComponent.OwnedEntity.Value, new NewRevStageEvent());
                }
                if (headRevsNames.Length > 0) // fucked
                    headRevsNames = headRevsNames[..^2];

                _chatSystem.DispatchGlobalAnnouncement(Loc.GetString("rev-alert-stage-massacre-start", ("headRevsNames", headRevsNames)), colorOverride: Color.Red, usePresetTTS: true);
            }
            else if (component.Stage == RevolutionaryStage.Massacre && CheckRevsLose())
            {
                _chatSystem.DispatchGlobalAnnouncement(Loc.GetString("rev-alert-stage-massacre-end-with-rev-lost"), colorOverride: Color.Green, usePresetTTS: true);
                _roundEnd.DoRoundEndBehavior(RoundEndBehavior.ShuttleCall, component.ShuttleCallTime);
                GameTicker.EndGameRule(uid, gameRule);
            }
        }
    }

    protected override void AppendRoundEndText(EntityUid uid,
        RevolutionaryRuleComponent component,
        GameRuleComponent gameRule,
        ref RoundEndTextAppendEvent args)
    {
        base.AppendRoundEndText(uid, component, gameRule, ref args);

        var revsLost = CheckRevsLose();
        var commandLost = CheckCommandLose();
        // This is (revsLost, commandsLost) concatted together
        // (moony wrote this comment idk what it means)
        var index = (commandLost ? 1 : 0) | (revsLost ? 2 : 0);
        args.AddLine(Loc.GetString(Outcomes[index]));

        var sessionData = _antag.GetAntagIdentifiers(uid);
        args.AddLine(Loc.GetString("rev-headrev-count", ("initialCount", sessionData.Count)));
        foreach (var (mind, data, name) in sessionData)
        {
            _role.MindHasRole<RevolutionaryRoleComponent>(mind, out var role);
            var count = CompOrNull<RevolutionaryRoleComponent>(role)?.ConvertedCount ?? 0;

            args.AddLine(Loc.GetString("rev-headrev-name-user",
                ("name", name),
                ("username", data.UserName),
                ("count", count)));

            // TODO: someone suggested listing all alive? revs maybe implement at some point
        }
    }

    private void OnGetBriefing(EntityUid uid, RevolutionaryRoleComponent comp, ref GetBriefingEvent args)
    {
        var ent = args.Mind.Comp.OwnedEntity;
        var head = HasComp<HeadRevolutionaryComponent>(ent);
        args.Append(Loc.GetString(head ? "head-rev-briefing" : "rev-briefing"));
    }

    private void OnPendingMapInit(EntityUid uid, HeadRevolutionaryComponent comp, MapInitEvent args)
    {
        _actions.AddAction(uid, comp.HeadRevConvertAction, comp.HeadRevConvertActionEntity);
    }

    /// <summary>
    /// Called when a Head Rev clicks on player using ability.
    /// </summary>
    private void OnTargetWithConvertWindow(EntityUid uid, HeadRevolutionaryComponent comp, ref HeadRevConvertActionEvent ev)
    {
        var alwaysConvertible = HasComp<AlwaysRevolutionaryConvertibleComponent>(ev.Target);
        var targetName = MetaData(ev.Target).EntityName;

        if (!_mind.TryGetMind(ev.Target, out var mindId, out var mind) && !alwaysConvertible)
            return;

        if (HasComp<RevolutionaryComponent>(ev.Target) ||
            HasComp<MindShieldComponent>(ev.Target) ||
            !HasComp<HumanoidAppearanceComponent>(ev.Target) &&
            !alwaysConvertible ||
            !_mobState.IsAlive(ev.Target) ||
            HasComp<ZombieComponent>(ev.Target))
        {
            _popup.PopupEntity(Loc.GetString("head-rev-cant-convert-attempt", ("target", targetName)), ev.Target, uid);
            return;
        }

        if (mind == null || _role.MindHasRole<RevolutionaryRoleComponent>(mindId))
        {
            _popup.PopupEntity(Loc.GetString("head-rev-cant-convert-attempt", ("target", targetName)), ev.Target, uid);
            return;
        }

        // Yes, we still need to track down the client because we need to open the Eui
        if (mind.UserId == null || !_playerManager.TryGetSessionById(mind.UserId.Value, out var client))
        {
            _popup.PopupEntity(Loc.GetString("head-rev-cant-convert-attempt", ("target", targetName)), ev.Target, uid);
            return; // If we can't track down the client, we can't offer transfer. That'd be quite bad.
        }

        _adminLogManager.Add(LogType.Mind,
            LogImpact.Medium,
            $"{ToPrettyString(ev.Performer)} sended invite to {ToPrettyString(ev.Target)} into a Revolutionary");

        _popup.PopupEntity(Loc.GetString("head-rev-on-convert-attempt", ("target", targetName)), ev.Target, uid);

        _euiMan.OpenEui(new BecomeRevEui(uid, ev.Target, this), client);
    }

    /// <summary>
    /// Called when a Head Rev uses a flash in melee to convert somebody else.
    /// </summary>
    public void Convert(EntityUid headRevUid, EntityUid targetUid)
    {
        var alwaysConvertible = HasComp<AlwaysRevolutionaryConvertibleComponent>(targetUid);

        if (!_mind.TryGetMind(targetUid, out var mindId, out var mind) && !alwaysConvertible)
            return;

        if (HasComp<RevolutionaryComponent>(targetUid) ||
            HasComp<MindShieldComponent>(targetUid) ||
            !HasComp<HumanoidAppearanceComponent>(targetUid) &&
            !alwaysConvertible ||
            !_mobState.IsAlive(targetUid) ||
            HasComp<ZombieComponent>(targetUid))
        {
            return;
        }

        _npcFaction.AddFaction(targetUid, RevolutionaryNpcFaction);
        var revComp = EnsureComp<RevolutionaryComponent>(targetUid);

        _adminLogManager.Add(LogType.Mind,
            LogImpact.Medium,
            $"{ToPrettyString(headRevUid)} converted {ToPrettyString(targetUid)} into a Revolutionary");

        if (_mind.TryGetMind(headRevUid, out var revMindId, out _))
        {
            if (_role.MindHasRole<RevolutionaryRoleComponent>(revMindId, out var role))
                role.Value.Comp2.ConvertedCount++;
        }

        if (mindId == default || !_role.MindHasRole<RevolutionaryRoleComponent>(mindId))
        {
            _role.MindAddRole(mindId, "MindRoleRevolutionary");
        }

        if (mind?.Session != null)
            _antag.SendBriefing(mind.Session, Loc.GetString("rev-role-greeting"), Color.Red, revComp.RevStartSound);
    }

    /// <summary>
    /// Called when a Head Rev uses a flash in melee to convert somebody else.
    /// </summary>
    private void OnPostFlash(EntityUid uid, HeadRevolutionaryComponent comp, ref AfterFlashedEvent ev)
    {
        if (comp.MassacreStage == false)
            return;

        var alwaysConvertible = HasComp<AlwaysRevolutionaryConvertibleComponent>(ev.Target);

        if (!_mind.TryGetMind(ev.Target, out var mindId, out var mind) && !alwaysConvertible)
            return;

        if (HasComp<RevolutionaryComponent>(ev.Target) ||
            HasComp<MindShieldComponent>(ev.Target) ||
            !HasComp<HumanoidAppearanceComponent>(ev.Target) &&
            !alwaysConvertible ||
            !_mobState.IsAlive(ev.Target) ||
            HasComp<ZombieComponent>(ev.Target))
        {
            return;
        }

        _npcFaction.AddFaction(ev.Target, RevolutionaryNpcFaction);
        var revComp = EnsureComp<RevolutionaryComponent>(ev.Target);

        if (ev.User != null)
        {
            _adminLogManager.Add(LogType.Mind,
                LogImpact.Medium,
                $"{ToPrettyString(ev.User.Value)} converted {ToPrettyString(ev.Target)} into a Revolutionary");

            if (_mind.TryGetMind(ev.User.Value, out var revMindId, out _))
            {
                if (_role.MindHasRole<RevolutionaryRoleComponent>(revMindId, out var role))
                    role.Value.Comp2.ConvertedCount++;
            }
        }

        if (mindId == default || !_role.MindHasRole<RevolutionaryRoleComponent>(mindId))
        {
            _role.MindAddRole(mindId, "MindRoleRevolutionary");
        }

        if (mind?.Session != null)
            _antag.SendBriefing(mind.Session, Loc.GetString("rev-role-greeting"), Color.Red, revComp.RevStartSound);
    }

    //TODO: Enemies of the revolution
    private void OnCommandMobStateChanged(EntityUid uid, CommandStaffComponent comp, MobStateChangedEvent ev)
    {
        if (ev.NewMobState == MobState.Dead || ev.NewMobState == MobState.Invalid)
            CheckCommandLose();
    }

    /// <summary>
    /// Checks if all of command is dead and if so will remove all sec and command jobs if there were any left.
    /// </summary>
    private bool CheckCommandLose()
    {
        var commandList = new List<EntityUid>();

        var heads = AllEntityQuery<CommandStaffComponent>();
        while (heads.MoveNext(out var id, out _))
        {
            commandList.Add(id);
        }

        return IsGroupDetainedOrDead(commandList, true, true, true);
    }

    /// <summary>
    /// Get the fraction of players that join revolutionary, between 0 and 1
    /// </summary>
    private float GetRevsFraction()
    {
        var players = GetHealthyHumanoids();
        var revsCount = 0;
        var query = EntityQueryEnumerator<HumanoidAppearanceComponent, RevolutionaryComponent>();
        while (query.MoveNext(out _, out _, out _))
        {
            revsCount++;
        }

        return revsCount / (float)players.Count;
    }

    /// <summary>
    /// Gets the list of humanoids who are alive and are on a station.
    /// Flying off via a shuttle disqualifies you.
    /// </summary>
    /// <returns></returns>
    private List<EntityUid> GetHealthyHumanoids()
    {
        var humanoids = new List<EntityUid>();
        var stationGrids = new HashSet<EntityUid>();

        foreach (var station in _stationSystem.GetStationsSet())
        {
            if (TryComp<StationDataComponent>(station, out var data) && _stationSystem.GetLargestGrid(data) is { } grid)
                stationGrids.Add(grid);
        }

        var players = AllEntityQuery<HumanoidAppearanceComponent, ActorComponent, MobStateComponent, TransformComponent>();
        while (players.MoveNext(out var uid, out _, out _, out var mob, out var xform))
        {
            if (!_mobState.IsAlive(uid, mob))
                continue;

            if (!stationGrids.Contains(xform.GridUid ?? EntityUid.Invalid))
                continue;

            humanoids.Add(uid);
        }
        return humanoids;
    }

    private void OnHeadRevMobStateChanged(EntityUid uid, HeadRevolutionaryComponent comp, MobStateChangedEvent ev)
    {
        if (ev.NewMobState == MobState.Dead || ev.NewMobState == MobState.Invalid)
            CheckRevsLose();
    }

    /// <summary>
    /// Checks if all the Head Revs are dead and if so will deconvert all regular revs.
    /// </summary>
    private bool CheckRevsLose()
    {
        var stunTime = TimeSpan.FromSeconds(4);
        var headRevList = new List<EntityUid>();

        var headRevs = AllEntityQuery<HeadRevolutionaryComponent, MobStateComponent>();
        while (headRevs.MoveNext(out var uid, out _, out _))
        {
            headRevList.Add(uid);
        }

        // If no Head Revs are alive all normal Revs will lose their Rev status and rejoin Nanotrasen
        // Cuffing Head Revs is not enough - they must be killed.
        if (IsGroupDetainedOrDead(headRevList, false, false, false))
        {
            var rev = AllEntityQuery<RevolutionaryComponent, MindContainerComponent>();
            while (rev.MoveNext(out var uid, out _, out var mc))
            {
                if (HasComp<HeadRevolutionaryComponent>(uid))
                    continue;

                _npcFaction.RemoveFaction(uid, RevolutionaryNpcFaction);
                _stun.TryParalyze(uid, stunTime, true);
                RemCompDeferred<RevolutionaryComponent>(uid);
                _popup.PopupEntity(Loc.GetString("rev-break-control", ("name", Identity.Entity(uid, EntityManager))), uid);
                _adminLogManager.Add(LogType.Mind, LogImpact.Medium, $"{ToPrettyString(uid)} was deconverted due to all Head Revolutionaries dying.");

                if (!_mind.TryGetMind(uid, out var mindId, out _, mc))
                    continue;

                // remove their antag role
                _role.MindTryRemoveRole<RevolutionaryRoleComponent>(mindId);

                // make it very obvious to the rev they've been deconverted since
                // they may not see the popup due to antag and/or new player tunnel vision
                if (_mind.TryGetSession(mindId, out var session))
                    _euiMan.OpenEui(new DeconvertedEui(), session);
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Will take a group of entities and check if these entities are alive, dead or cuffed.
    /// </summary>
    /// <param name="list">The list of the entities</param>
    /// <param name="checkOffStation">Bool for if you want to check if someone is in space and consider them missing in action. (Won't check when emergency shuttle arrives just in case)</param>
    /// <param name="countCuffed">Bool for if you don't want to count cuffed entities.</param>
    /// <param name="countRevolutionaries">Bool for if you want to count revolutionaries.</param>
    /// <returns></returns>
    private bool IsGroupDetainedOrDead(List<EntityUid> list, bool checkOffStation, bool countCuffed, bool countRevolutionaries)
    {
        var gone = 0;

        foreach (var entity in list)
        {
            if (TryComp<CuffableComponent>(entity, out var cuffed) && cuffed.CuffedHandCount > 0 && countCuffed)
            {
                gone++;
                continue;
            }

            if (TryComp<MobStateComponent>(entity, out var state))
            {
                if (state.CurrentState == MobState.Dead || state.CurrentState == MobState.Invalid)
                {
                    gone++;
                    continue;
                }

                if (checkOffStation && _stationSystem.GetOwningStation(entity) == null && !_emergencyShuttle.EmergencyShuttleArrived)
                {
                    gone++;
                    continue;
                }
            }
            //If they don't have the MobStateComponent they might as well be dead.
            else
            {
                gone++;
                continue;
            }

            if ((HasComp<RevolutionaryComponent>(entity) || HasComp<HeadRevolutionaryComponent>(entity)) && countRevolutionaries)
            {
                gone++;
                continue;
            }
        }

        return gone == list.Count || list.Count == 0;
    }

    private static readonly string[] Outcomes =
    {
        // revs survived and heads survived... how
        "rev-reverse-stalemate",
        // revs won and heads died
        "rev-won",
        // revs lost and heads survived
        "rev-lost",
        // revs lost and heads died
        "rev-stalemate"
    };
}
