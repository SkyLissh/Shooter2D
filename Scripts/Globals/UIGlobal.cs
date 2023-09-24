using Godot;

namespace Shooter2D.Scripts.Globals;

public partial class UIGlobal : Node
{
  [Signal]
  public delegate void HealthChangedEventHandler();

  [Signal]
  public delegate void LaserCountChangedEventHandler();

  [Signal]
  public delegate void GranadeCountChangedEventHandler();

  private int _health = 50;
  private int _laserCount = 20;
  private int _granadeCount = 5;

  public int LaserCount
  {
    get => _laserCount;
    set
    {
      _laserCount = value;
      EmitSignal(SignalName.LaserCountChanged);
    }
  }

  public int GranadeCount
  {
    get => _granadeCount;
    set
    {
      _granadeCount = value;
      EmitSignal(SignalName.GranadeCountChanged);
    }
  }

  public int Health
  {
    get => _health;
    set
    {
      _health = value;
      EmitSignal(SignalName.HealthChanged);
    }
  }
}
