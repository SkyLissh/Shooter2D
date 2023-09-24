using System.Collections.Immutable;
using System.Linq;
using Godot;
using Godot.Collections;
using Shooter2D.Scripts.Config.KeyBinds;
using Shooter2D.Scripts.Globals;
using Shooter2D.Scripts.Items;

namespace Shooter2D.Scripts.Player;

public partial class PlayerController : CharacterBody2D
{
  [Signal]
  public delegate void GrenadeThrowEventHandler(Vector2 position, Vector2 direction);

  [Signal]
  public delegate void ShootEventHandler(Vector2 position, Vector2 direction);

  private UIGlobal _uiGlobals;

  private bool _canShoot = true;

  private bool _canThrowGrenade = true;

  private Timer _grenadeTimer;

  private GpuParticles2D _gunParticles;

  private ImmutableArray<Marker2D> _laserMarkers;
  private Timer _shootTimer;

  [Export]
  private int MaxSpeed { get; set; } = 400;
  public int Speed { get; set; }

  public override void _Ready()
  {
    Speed = MaxSpeed;

    _grenadeTimer = GetNode<Timer>("GrenadeTimer");
    _shootTimer = GetNode<Timer>("ShootTimer");
    _gunParticles = GetNode<GpuParticles2D>("GunParticles");
    _uiGlobals = GetNode<UIGlobal>("/root/UIGlobals");

    Array<Node> laserMarkers = GetNode<Node2D>("LaserMarkers").GetChildren();
    _laserMarkers = laserMarkers.Select(e => e).Cast<Marker2D>().ToImmutableArray();
  }

  public override void _Process(double delta)
  {
    if (Input.IsActionPressed(WeaponBinds.Shoot))
      OnShoot();

    if (Input.IsActionPressed(WeaponBinds.Grenade))
      OnThrowGrenade();
  }

  public override void _PhysicsProcess(double delta)
  {
    LookAt(GetGlobalMousePosition());

    var direction = Input.GetVector(
      MovementBinds.Left,
      MovementBinds.Right,
      MovementBinds.Up,
      MovementBinds.Down
    );

    if (direction == Vector2.Zero)
      return;

    Velocity = direction.Normalized() * Speed;

    MoveAndSlide();
  }

  private void OnShoot()
  {
    if (!_canShoot || _uiGlobals.LaserCount <= 0)
      return;

    _uiGlobals.LaserCount--;

    _gunParticles.Emitting = true;

    var marker = _laserMarkers[(int)(GD.Randi() % _laserMarkers.Length)];
    var direction = GetGlobalMousePosition() - marker.GlobalPosition;

    EmitSignal(SignalName.Shoot, marker.GlobalPosition, direction.Normalized());
    _canShoot = false;
    _shootTimer.Start();
  }

  private void OnThrowGrenade()
  {
    if (!_canThrowGrenade || _uiGlobals.GranadeCount <= 0)
      return;

    _uiGlobals.GranadeCount--;

    var direction = GetGlobalMousePosition() - Position;

    EmitSignal(
      SignalName.GrenadeThrow,
      _laserMarkers[0].GlobalPosition,
      direction.Normalized()
    );
    _canThrowGrenade = false;
    _grenadeTimer.Start();
  }

  private void OnGrenadeTimeout()
  {
    _canThrowGrenade = true;
  }

  private void OnShootTimeout()
  {
    _canShoot = true;
  }

  private void OnEnteredBuilding(Node2D _)
  {
    var tween = GetTree().CreateTween();
    var camera = GetNode<Camera2D>("Camera2D");

    tween.TweenProperty(
      camera,
      nameof(camera.Zoom).ToLower(),
      new Vector2(1.4f, 1.4f),
      1
    );
  }

  private void OnLeftBuilding(Node2D _)
  {
    var tween = GetTree().CreateTween();
    var camera = GetNode<Camera2D>("Camera2D");

    tween.TweenProperty(camera, nameof(camera.Zoom).ToLower(), new Vector2(1, 1), 1);
  }

  public void AddItem(ItemOptions item)
  {
    switch (item)
    {
      case ItemOptions.Laser:
        _uiGlobals.LaserCount += 5;
        break;
      case ItemOptions.Grenade:
        _uiGlobals.GranadeCount += 1;
        break;
      case ItemOptions.Health:
        _uiGlobals.Health += 10;
        break;
    }
  }
}
