using Godot;

namespace Shooter2D.Scripts.Gate;

public partial class StaticTravelController : StaticBody2D
{
  [Signal]
  public delegate void PlayerEnteredEventHandler(string levelPath);

  [Export]
  protected string LevelPath { get; set; }

  private void OnBodyEntered(Node _) => EmitSignal(SignalName.PlayerEntered, LevelPath);
}
