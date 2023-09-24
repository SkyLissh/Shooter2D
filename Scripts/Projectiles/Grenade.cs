using Godot;

namespace Shooter2D.Scripts.Projectiles;

public partial class Grenade : Explosive
{
  private readonly StringName _explosionAnimation = "Explosion";

  private AnimationPlayer _player;

  [Export] public override int Damage { get; protected set; } = 20;


  public override void _Ready()
  {
    _player = GetNode<AnimationPlayer>("AnimationPlayer");
  }

  private void Explode()
  {
    _player.Play(_explosionAnimation);
  }
}
