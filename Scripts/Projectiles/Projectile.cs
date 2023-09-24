using Godot;

using Shooter2D.Scripts.Enemies;

namespace Shooter2D.Scripts.Projectiles;

public abstract partial class Projectile : Area2D, IProjectile
{
  [Export] public virtual int Speed { get; protected set; } = 600;
  [Export] public virtual int Damage { get; protected set; } = 10;

  protected virtual void OnBodyEntered(Node2D body)
  {
    if (body is Enemy enemy) enemy.OnHit(Damage);
    QueueFree();
  }

  protected virtual void OnTimeout() => QueueFree();
}
