using Godot;

namespace Shooter2D.Scripts.Projectiles;

public abstract partial class Explosive : RigidBody2D, IProjectile
{
  [Export] public virtual int Speed { get; protected set; } = 600;
  [Export] public virtual int Damage { get; protected set; } = 30;
}
