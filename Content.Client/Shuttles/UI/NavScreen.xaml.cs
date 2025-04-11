using System.Numerics;
using Content.Shared.Shuttles.BUIStates;
using Robust.Client.AutoGenerated;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Map;
using Robust.Shared.Physics.Components;

namespace Content.Client.Shuttles.UI;

[GenerateTypedNameReferences]
public sealed partial class NavScreen : BoxContainer
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    private SharedTransformSystem _xformSystem;

    private EntityUid? _consoleEntity; // Entity of controlling console
    private EntityUid? _shuttleEntity;
    public event Action? OnSignalButtonPressed; // DS14

    public event Action<NetEntity, float, float>? ThrustersRestartRequest;

    public NavScreen()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);
        _xformSystem = _entManager.System<SharedTransformSystem>();

        IFFToggle.OnToggled += OnIFFTogglePressed;
        IFFToggle.Pressed = NavRadar.ShowIFF;

        DockToggle.OnToggled += OnDockTogglePressed;
        DockToggle.Pressed = NavRadar.ShowDocks;

        SignalButton.OnPressed += _ => OnSignalButtonPressed?.Invoke(); // DS14

        GyroscopesThrust.OnValueChanged += OnThrustersConfig;
        ThrustersThrust.OnValueChanged += OnThrustersConfig;
        // ThrustersRestartButton.OnPressed += OnThrustersConfig;
    }

    private void OnThrustersConfig(int _)
    {
        var netEntityShuttle = _entManager.GetNetEntity(_shuttleEntity);
        if (netEntityShuttle != null)
        {
            ThrustersRestartRequest?.Invoke(
                netEntityShuttle.Value,
                (float)GyroscopesThrust.Value,
                (float)ThrustersThrust.Value);
        }
    }

    public void SetShuttle(EntityUid? shuttle)
    {
        _shuttleEntity = shuttle;
    }

    public void SetConsole(EntityUid? console)
    {
        _consoleEntity = console;
        NavRadar.SetConsole(console);
    }

    private void OnIFFTogglePressed(BaseButton.ButtonEventArgs args)
    {
        NavRadar.ShowIFF ^= true;
        args.Button.Pressed = NavRadar.ShowIFF;
    }

    private void OnDockTogglePressed(BaseButton.ButtonEventArgs args)
    {
        NavRadar.ShowDocks ^= true;
        args.Button.Pressed = NavRadar.ShowDocks;
    }

    public void UpdateState(NavInterfaceState scc)
    {
        NavRadar.UpdateState(scc);
    }

    public void SetMatrix(EntityCoordinates? coordinates, Angle? angle)
    {
        _shuttleEntity = coordinates?.EntityId;
        NavRadar.SetMatrix(coordinates, angle);
    }

    protected override void Draw(DrawingHandleScreen handle)
    {
        base.Draw(handle);

        if (!_entManager.TryGetComponent(_shuttleEntity, out TransformComponent? gridXform) ||
            !_entManager.TryGetComponent(_shuttleEntity, out PhysicsComponent? gridBody))
        {
            return;
        }

        var (_, worldRot, worldMatrix) = _xformSystem.GetWorldPositionRotationMatrix(gridXform);
        var worldPos = Vector2.Transform(gridBody.LocalCenter, worldMatrix);

        // Get the positive reduced angle.
        var displayRot = -worldRot.Reduced();

        GridPosition.Text = Loc.GetString("shuttle-console-position-value",
            ("X", $"{worldPos.X:0.0}"),
            ("Y", $"{worldPos.Y:0.0}"));
        GridOrientation.Text = Loc.GetString("shuttle-console-orientation-value",
            ("angle", $"{displayRot.Degrees:0.0}"));

        var gridVelocity = gridBody.LinearVelocity;
        gridVelocity = displayRot.RotateVec(gridVelocity);
        // Get linear velocity relative to the console entity
        GridLinearVelocity.Text = Loc.GetString("shuttle-console-linear-velocity-value",
            ("X", $"{gridVelocity.X + 10f * float.Epsilon:0.0}"),
            ("Y", $"{gridVelocity.Y + 10f * float.Epsilon:0.0}"));
        GridAngularVelocity.Text = Loc.GetString("shuttle-console-angular-velocity-value",
            ("angularVelocity", $"{-MathHelper.RadiansToDegrees(gridBody.AngularVelocity) + 10f * float.Epsilon:0.0}"));
    }
}

