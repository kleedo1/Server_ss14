using Robust.Client.UserInterface;
using System.Numerics;
using Robust.Client.AutoGenerated;
using Robust.Client.Graphics;
using Robust.Shared.Utility;
using Robust.Client.GameObjects;
using Robust.Shared.Timing;
using Robust.Client.UserInterface.XAML;
using Robust.Client.Input;

namespace Content.Client.UserInterface.Controls;

[GenerateTypedNameReferences]
public partial class SimpleRadialMenu : RadialMenu
{
    private EntityUid? _attachMenuToEntity;

    [Dependency] private readonly IClyde _clyde = default!;
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] private readonly IInputManager _inputManager = default!;

    public SimpleRadialMenu()
    {
        IoCManager.InjectDependencies(this);
        RobustXamlLoader.Load(this);
    }

    public void Track(EntityUid owner)
    {
        _attachMenuToEntity = owner;
    }

    public void SetButtons(IEnumerable<RadialMenuOption> models, SimpleRadialMenuSettings? settings = null)
    {
        ClearExistingChildrenRadialButtons();

        var sprites = _entManager.System<SpriteSystem>();
        Fill(models, sprites, Children, settings ?? new SimpleRadialMenuSettings());
    }

    public void OpenOverMouseScreenPosition()
    {
        var vpSize = _clyde.ScreenSize;
        OpenCenteredAt(_inputManager.MouseScreenPosition.Position / vpSize);
    }

    private void Fill(
        IEnumerable<RadialMenuOption> models,
        SpriteSystem sprites,
        ICollection<Control> rootControlChildren,
        SimpleRadialMenuSettings settings
    )
    {
        var rootContainer = new RadialContainer
        {
            HorizontalExpand = true,
            VerticalExpand = true,
            InitialRadius = settings.DefaultContainerRadius,
            ReserveSpaceForHiddenChildren = false,
            Visible = true
        };
        rootControlChildren.Add(rootContainer);

        foreach (var model in models)
        {
            if (model is RadialMenuNestedLayerOption nestedMenuModel)
            {
                var linkButton = RecursiveContainerExtraction(sprites, rootControlChildren, nestedMenuModel, settings);
                linkButton.Visible = true;
                rootContainer.AddChild(linkButton);
            }
            else
            {
                var rootButtons = ConvertToButton(model, sprites, settings, false);
                rootContainer.AddChild(rootButtons);
            }
        }
    }

    private RadialMenuTextureButton RecursiveContainerExtraction(
        SpriteSystem sprites,
        ICollection<Control> rootControlChildren,
        RadialMenuNestedLayerOption model,
        SimpleRadialMenuSettings settings
    )
    {
        var container = new RadialContainer
        {
            HorizontalExpand = true,
            VerticalExpand = true,
            InitialRadius = model.ContainerRadius!.Value,
            ReserveSpaceForHiddenChildren = false,
            Visible = false
        };
        foreach (var nested in model.Nested)
        {
            if (nested is RadialMenuNestedLayerOption nestedMenuModel)
            {
                var linkButton = RecursiveContainerExtraction(sprites, rootControlChildren, nestedMenuModel, settings);
                container.AddChild(linkButton);
            }
            else
            {
                var button = ConvertToButton(nested, sprites, settings, false);
                container.AddChild(button);
            }
        }
        rootControlChildren.Add(container);

        var thisLayerLinkButton = ConvertToButton(model, sprites, settings, true);
        thisLayerLinkButton.TargetLayer = container;
        return thisLayerLinkButton;
    }

    private RadialMenuTextureButton ConvertToButton(
        RadialMenuOption model,
        SpriteSystem sprites,
        SimpleRadialMenuSettings settings,
        bool haveNested
    )
    {
        var button = settings.UseSectors
            ? ConvertToButtonWithSector(model, settings)
            : new RadialMenuTextureButton();
        button.SetSize = new Vector2(64f, 64f);
        button.ToolTip = model.ToolTip;
        if (model.Sprite != null)
        {
            var scale = Vector2.One;

            var texture = sprites.Frame0(model.Sprite);
            if (texture.Width <= 32)
            {
                scale *= 2;
            }

            button.TextureNormal = texture;
            button.Scale = scale;
        }

        if (model is RadialMenuActionOption actionOption)
        {
            button.OnPressed += _ =>
            {
                actionOption.OnPressed?.Invoke();
                if(!haveNested)
                    Close();
            };
        }
        
        return button;
    }

    private static RadialMenuTextureButtonWithSector ConvertToButtonWithSector(RadialMenuOption model, SimpleRadialMenuSettings settings)
    {
        var button = new RadialMenuTextureButtonWithSector
        {
            DrawBorder = settings.DisplayBorders,
            DrawBackground = !settings.NoBackground
        };
        if (model.BackgroundColor.HasValue)
        {
            button.BackgroundColor = model.BackgroundColor.Value;
        }

        if (model.HoverBackgroundColor.HasValue)
        {
            button.HoverBackgroundColor = model.HoverBackgroundColor.Value;
        }

        return button;
    }

    private void ClearExistingChildrenRadialButtons()
    {
        var toRemove = new List<Control>(ChildCount);
        foreach (var child in Children)
        {
            if (child != ContextualButton && child != MenuOuterAreaButton)
            {
                toRemove.Add(child);
            }
        }

        foreach (var control in toRemove)
        {
            Children.Remove(control);
        }
    }

    #region target entity tracking

    protected override void FrameUpdate(FrameEventArgs args)
    {
        base.FrameUpdate(args);
        if (_attachMenuToEntity != null)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        if (!_entManager.TryGetComponent(_attachMenuToEntity, out TransformComponent? xform))
        {
            Close();
            return;
        }

        if (!xform.Coordinates.IsValid(_entManager))
        {
            Close();
            return;
        }

        var coords = _entManager.System<SpriteSystem>().GetSpriteScreenCoordinates((_attachMenuToEntity.Value, null, xform));

        if (!coords.IsValid)
        {
            Close();
            return;
        }

        OpenScreenAt(coords.Position, _clyde);
    }

    #endregion

}


public abstract class RadialMenuOption
{
    public string? ToolTip { get; init; }
    
    public SpriteSpecifier? Sprite { get; init; }
    public Color? BackgroundColor { get; set; }
    public Color? HoverBackgroundColor { get; set; }
}

public class RadialMenuActionOption(Action onPressed) : RadialMenuOption
{
    public Action OnPressed { get; } = onPressed;
}

public class RadialMenuActionOption<T>(Action<T> onPressed, T data)
    : RadialMenuActionOption(onPressed: () => onPressed(data));

public class RadialMenuNestedLayerOption(IReadOnlyCollection<RadialMenuOption> nested, float containerRadius = 100)
    : RadialMenuOption
{
    public float? ContainerRadius { get; } = containerRadius;

    public IReadOnlyCollection<RadialMenuOption> Nested { get; } = nested;
}

public class SimpleRadialMenuSettings
{
    /// <summary>
    /// Default container draw radius. Is going to be further affected by per sector increment.
    /// </summary>
    public int DefaultContainerRadius = 100;

    /// <summary>
    /// Marker, if sector-buttons should be used.
    /// </summary>
    public bool UseSectors = true;

    /// <summary>
    /// Marker, if border of buttons should be rendered. Can only be used when <see cref="UseSectors"/> = true.
    /// </summary>
    public bool DisplayBorders = true;

    /// <summary>
    /// Marker, if sector background should not be rendered. Can only be used when <see cref="UseSectors"/> = true.
    /// </summary>
    public bool NoBackground = false;
}

