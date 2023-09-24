using Godot;
using Shooter2D.Scripts.Player;
using Shooter2D.Scripts.Projectiles;
using Shooter2D.Scripts.UI;

namespace Shooter2D.Scripts.Levels;

public partial class Level : Node2D
{
  private readonly PackedScene _grenadeScene = ResourceLoader.Load<PackedScene>(
    "res://Scenes/Projectiles/Grenade.tscn"
  );

  private readonly PackedScene _laserScene = ResourceLoader.Load<PackedScene>(
    "res://Scenes/Projectiles/Laser.tscn"
  );

  private PlayerController _player;

  private Node2D _projectiles;

  private UIPlayer _ui;

  private TransitionLayer _transitionLayer;

  public override void _Ready()
  {
    _projectiles = GetNode<Node2D>("Projectiles");
    _player = GetNode<PlayerController>("Player");
    _ui = GetNode<UIPlayer>("UI");
    _transitionLayer = GetNode<TransitionLayer>("/root/TransitionLayer");
  }

  private void OnPlayerEnteredTravel(string levelPath)
  {
    var tween = GetTree().CreateTween();
    tween.TweenProperty(_player, nameof(_player.Speed), 0, 0.5f);
    tween.Finished += async () => await _transitionLayer.ChangeScene(levelPath);
  }

  private void OnLaserShoot(Vector2 position, Vector2 direction)
  {
    var laser = _laserScene.Instantiate<Laser>();
    laser.Position = position;
    laser.Rotation = direction.Angle();
    laser.Direction = direction;
    _projectiles.AddChild(laser);
    _ui.UpdateLaserCount();
  }

  private void OnGrenadeThrow(Vector2 position, Vector2 direction)
  {
    var grenade = _grenadeScene.Instantiate<Grenade>();
    grenade.Position = position;
    grenade.LinearVelocity = direction * grenade.Speed;
    _projectiles.AddChild(grenade);
    _ui.UpdateGranadeCount();
  }

  private void OnUpdateStats()
  {
    _ui.UpdateLaserCount();
    _ui.UpdateGranadeCount();
  }
}
