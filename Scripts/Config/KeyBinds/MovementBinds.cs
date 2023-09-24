using Godot;

namespace Shooter2D.Scripts.Config.KeyBinds;
public abstract record MovementBinds
{
  public static readonly StringName Right = "right";
  public static readonly StringName Up = "up";
  public static readonly StringName Down = "down";
  public static readonly StringName Left = "left";
}
