using Godot;

namespace Shooter2D.Scripts.Config.KeyBinds;

public abstract record WeaponBinds
{
  public static readonly StringName Shoot = "shoot";
  public static readonly StringName Grenade = "grenade";
}
