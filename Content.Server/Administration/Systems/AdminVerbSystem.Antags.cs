using Content.Server.Administration.Commands;
using Content.Server.Antag;
using Content.Server.GameTicking;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.Zombies;
using Content.Shared.Administration;
using Content.Shared.Database;
using Content.Shared.Humanoid;
using Content.Shared.Mind.Components;
using Content.Shared.Roles;
using Content.Shared.Verbs;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;
using Content.Shared.DeadSpace.Events.Roles.Components;
using Content.Server.GameTicking.Rules;

namespace Content.Server.Administration.Systems;

public sealed partial class AdminVerbSystem
{
    [Dependency] private readonly AntagSelectionSystem _antag = default!;
    [Dependency] private readonly ZombieSystem _zombie = default!;
    [Dependency] private readonly GameTicker _gameTicker = default!;
    [Dependency] private readonly UnitologyRuleSystem _unitologyRule = default!;

    [ValidatePrototypeId<EntityPrototype>]
    private const string DefaultTraitorRule = "Traitor";

    [ValidatePrototypeId<EntityPrototype>]
    private const string UnitologyRule = "Unitology";

    [ValidatePrototypeId<EntityPrototype>]
    private const string SpiderTerrorRule = "SpiderTerror";

    [ValidatePrototypeId<EntityPrototype>]
    private const string DefaultInitialInfectedRule = "Zombie";

    [ValidatePrototypeId<EntityPrototype>]
    private const string DefaultNukeOpRule = "LoneOpsSpawn";

    [ValidatePrototypeId<EntityPrototype>]
    private const string DefaultRevsRule = "Revolutionary";

    [ValidatePrototypeId<EntityPrototype>]
    private const string DefaultThiefRule = "Thief";

    [ValidatePrototypeId<StartingGearPrototype>]
    private const string PirateGearId = "PirateGear";

    private readonly EntProtoId _paradoxCloneRuleId = "ParadoxCloneSpawn";

    // All antag verbs have names so invokeverb works.
    private void AddAntagVerbs(GetVerbsEvent<Verb> args)
    {
        if (!TryComp<ActorComponent>(args.User, out var actor))
            return;

        var player = actor.PlayerSession;

        if (!_adminManager.HasAdminFlag(player, AdminFlags.Fun))
            return;

        if (!HasComp<MindContainerComponent>(args.Target) || !TryComp<ActorComponent>(args.Target, out var targetActor))
            return;

        var targetPlayer = targetActor.PlayerSession;

        var traitorName = Loc.GetString("admin-verb-text-make-traitor");
        Verb traitor = new()
        {
            Text = traitorName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/Interface/Misc/job_icons.rsi"), "Syndicate"),
            Act = () =>
            {
                _antag.ForceMakeAntag<TraitorRuleComponent>(targetPlayer, DefaultTraitorRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", traitorName,  Loc.GetString("admin-verb-make-traitor")),
        };
        args.Verbs.Add(traitor);

        var initialInfectedName = Loc.GetString("admin-verb-text-make-initial-infected");
        Verb initialInfected = new()
        {
            Text = initialInfectedName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/Interface/Misc/job_icons.rsi"), "InitialInfected"),
            Act = () =>
            {
                _antag.ForceMakeAntag<ZombieRuleComponent>(targetPlayer, DefaultInitialInfectedRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", initialInfectedName, Loc.GetString("admin-verb-make-initial-infected")),
        };
        args.Verbs.Add(initialInfected);

        // DS14-start
        var blobName = Loc.GetString("admin-verb-text-make-blob");
        Verb blob = new()
        {
            Text = blobName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/_Backmen/Interface/Actions/blob.rsi"), "blobFactory"),
            Act = () =>
            {
                EnsureComp<Shared.Backmen.Blob.Components.BlobCarrierComponent>(args.Target).HasMind = HasComp<ActorComponent>(args.Target);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", blobName, Loc.GetString("admin-verb-make-blob")),
        };
        args.Verbs.Add(blob);
        // DS14-end

        var zombieName = Loc.GetString("admin-verb-text-make-zombie");
        Verb zombie = new()
        {
            Text = zombieName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/Interface/Misc/job_icons.rsi"), "Zombie"),
            Act = () =>
            {
                _zombie.ZombifyEntity(args.Target);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", zombieName, Loc.GetString("admin-verb-make-zombie")),
        };
        args.Verbs.Add(zombie);

        var nukeOpName = Loc.GetString("admin-verb-text-make-nuclear-operative");
        Verb nukeOp = new()
        {
            Text = nukeOpName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/Clothing/Head/Hardsuits/syndicate.rsi"), "icon"),
            Act = () =>
            {
                _antag.ForceMakeAntag<NukeopsRuleComponent>(targetPlayer, DefaultNukeOpRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", nukeOpName, Loc.GetString("admin-verb-make-nuclear-operative")),
        };
        args.Verbs.Add(nukeOp);

        var pirateName = Loc.GetString("admin-verb-text-make-pirate");
        Verb pirate = new()
        {
            Text = pirateName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/Clothing/Head/Hats/pirate.rsi"), "icon"),
            Act = () =>
            {
                // pirates just get an outfit because they don't really have logic associated with them
                SetOutfitCommand.SetOutfit(args.Target, PirateGearId, EntityManager);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", pirateName, Loc.GetString("admin-verb-make-pirate")),
        };
        args.Verbs.Add(pirate);

        var headRevName = Loc.GetString("admin-verb-text-make-head-rev");
        Verb headRev = new()
        {
            Text = headRevName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/Interface/Misc/job_icons.rsi"), "HeadRevolutionary"),
            Act = () =>
            {
                _antag.ForceMakeAntag<RevolutionaryRuleComponent>(targetPlayer, DefaultRevsRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", headRevName, Loc.GetString("admin-verb-make-head-rev")),
        };
        args.Verbs.Add(headRev);

        // DS14-start
        var uniName = Loc.GetString("admin-verb-text-make-unitolog");
        Verb uni = new()
        {
            Text = uniName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_DeadSpace/Interface/Misc/antag_icons.rsi"), "Unitology"),
            Act = () =>
            {
                _antag.ForceMakeAntag<UnitologyRuleComponent>(targetPlayer, UnitologyRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", uniName, Loc.GetString("admin-verb-make-unitolog")),
        };
        args.Verbs.Add(uni);

        var spiderTerrorName = Loc.GetString("admin-verb-text-make-spider-terror");
        Verb spiderTerror = new()
        {
            Text = spiderTerrorName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_DeadSpace/Interface/Misc/antag_icons.rsi"), "Egg"),
            Act = () =>
            {
                _antag.ForceMakeAntag<SpiderTerrorRuleComponent>(targetPlayer, SpiderTerrorRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", spiderTerrorName, Loc.GetString("admin-verb-make-spider-terror")),
        };
        args.Verbs.Add(spiderTerror);
        // DS14-end

        var thiefName = Loc.GetString("admin-verb-text-make-thief");
        Verb thief = new()
        {
            Text = thiefName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/Clothing/Hands/Gloves/Color/black.rsi"), "icon"),
            Act = () =>
            {
                _antag.ForceMakeAntag<ThiefRuleComponent>(targetPlayer, DefaultThiefRule);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", thiefName, Loc.GetString("admin-verb-make-thief")),
        };
        args.Verbs.Add(thief);

        var paradoxCloneName = Loc.GetString("admin-verb-text-make-paradox-clone");
        Verb paradox = new()
        {
            Text = paradoxCloneName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/Interface/Misc/job_icons.rsi"), "ParadoxClone"),
            Act = () =>
            {
                var ruleEnt = _gameTicker.AddGameRule(_paradoxCloneRuleId);

                if (!TryComp<ParadoxCloneRuleComponent>(ruleEnt, out var paradoxCloneRuleComp))
                    return;

                paradoxCloneRuleComp.OriginalBody = args.Target; // override the target player

                _gameTicker.StartGameRule(ruleEnt);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", paradoxCloneName, Loc.GetString("admin-verb-make-paradox-clone")),
        };

        if (HasComp<HumanoidAppearanceComponent>(args.Target)) // only humanoids can be cloned
            args.Verbs.Add(paradox);

        // DS14-start
        var eventRoleName = Loc.GetString("admin-verb-text-make-event-role");
        Verb eventRole = new()
        {
            Priority = -1,
            Text = eventRoleName,
            Category = VerbCategory.Antag,
            Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_DeadSpace/Interface/Misc/antag_icons.rsi"), "Event"),
            Act = () =>
            {
                if (HasComp<EventRoleComponent>(args.Target))
                    RemComp<EventRoleComponent>(args.Target);
                else
                    EnsureComp<EventRoleComponent>(args.Target);
            },
            Impact = LogImpact.High,
            Message = string.Join(": ", eventRoleName, Loc.GetString("admin-verb-make-event-role")),
        };
        args.Verbs.Add(eventRole);
        // DS14-end
    }
}
