// Мёртвый Космос, Licensed under custom terms with restrictions on public hosting and commercial use, full text: https://raw.githubusercontent.com/dead-space-server/space-station-14-fobos/master/LICENSE.TXT

using Content.Shared.Actions;
using Robust.Shared.Serialization;
using Content.Shared.DoAfter;

namespace Content.Shared.DeadSpace.Abilities.ChainAbility;

public sealed partial class ChainAbilityActionEvent : EntityTargetActionEvent
{

}


[Serializable, NetSerializable]
public sealed partial class ChainAbilityDoAfterEvent : SimpleDoAfterEvent
{

}
