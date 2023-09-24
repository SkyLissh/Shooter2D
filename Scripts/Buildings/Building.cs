using Godot;

namespace Shooter2D.Scripts.Buildings;

public partial class Building : Area2D
{
  [Signal] public delegate void PlayerEnteredEventHandler();

  private void OnPlayerEntered() => EmitSignal(SignalName.PlayerEntered);
}
