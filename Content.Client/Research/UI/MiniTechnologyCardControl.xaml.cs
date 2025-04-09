using Content.Shared.Research.Prototypes;
using Robust.Client.AutoGenerated;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Client.Research.UI;

[GenerateTypedNameReferences]
public sealed partial class MiniTechnologyCardControl : Control
{
    /// The technology that this control represents
    public readonly TechnologyPrototype Technology;

    public MiniTechnologyCardControl(TechnologyPrototype technology, IPrototypeManager prototypeManager, SpriteSystem spriteSys, FormattedMessage description)
    {
        RobustXamlLoader.Load(this);

        var discipline = prototypeManager.Index(technology.Discipline);
        Background.ModulateSelfOverride = discipline.Color;
        Texture.Texture = spriteSys.Frame0(technology.Icon);
        NameLabel.SetMessage(Loc.GetString(technology.Name));

        var tooltip = new Tooltip();
        tooltip.SetMessage(description);
        Main.TooltipSupplier = _ => tooltip;
        Technology = technology;
    }
}
