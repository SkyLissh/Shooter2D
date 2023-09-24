using Godot;
using Shooter2D.Scripts.Globals;

namespace Shooter2D.Scripts.UI;

public partial class UIPlayer : Node
{
  [Export]
  public Label LaserLabel;

  [Export]
  public TextureRect LaserIcon;

  [Export]
  public Label GranadeLabel;

  [Export]
  public TextureRect GranadeIcon;

  [Export]
  public TextureProgressBar HealthBar;

  private UIGlobal _uiGlobals;

  private Color _green = new("6bbfa3");
  private Color _red = new("f35353");

  public override void _Ready()
  {
    _uiGlobals = GetNode<UIGlobal>("/root/UIGlobals");
    _uiGlobals.HealthChanged += UpdateHealth;
    _uiGlobals.LaserCountChanged += UpdateLaserCount;
    _uiGlobals.GranadeCountChanged += UpdateGranadeCount;

    UpdateLaserCount();
    UpdateGranadeCount();
    UpdateHealth();
  }

  public void UpdateLaserCount()
  {
    LaserLabel.Text = _uiGlobals.LaserCount.ToString();
    UpdateColor(_uiGlobals.LaserCount, LaserLabel, LaserIcon);
  }

  public void UpdateGranadeCount()
  {
    GranadeLabel.Text = _uiGlobals.GranadeCount.ToString();
    UpdateColor(_uiGlobals.GranadeCount, GranadeLabel, GranadeIcon);
  }

  private void UpdateColor(int amount, Label label, TextureRect icon)
  {
    if (amount == 0)
    {
      label.Modulate = _red;
      icon.Modulate = _red;
      return;
    }

    label.Modulate = _green;
    icon.Modulate = _green;
  }

  private void UpdateHealth() => HealthBar.Value = _uiGlobals.Health;
}
