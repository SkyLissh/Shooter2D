using Godot;

namespace Shooter2D.Scripts.Enemies;

public abstract partial class Enemy : CharacterBody2D
{
  [Export] public int Life { get; protected set; } = 100;

  public virtual void OnHit(int damage)
  {
    Life -= damage;
    if (Life <= 0) QueueFree();
  }
}
