using Godot;

namespace Shooter2D.Scripts.Projectiles;

public partial class Laser : Projectile
{
  public Vector2 Direction { get; set; } = Vector2.Right;

  public override void _Process(double delta)
  {
    var velocity = Direction * Speed;
    Translate(velocity * (float)delta);
  }
}
