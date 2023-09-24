using System.Threading.Tasks;
using Godot;

namespace Shooter2D.Scripts.UI;

public partial class TransitionLayer : CanvasLayer
{
  [Export]
  public AnimationPlayer Animation { get; set; }

  public async Task ChangeScene(string levelPath)
  {
    Animation.Play("FadeIn");
    await ToSignal(Animation, AnimationPlayer.SignalName.AnimationFinished);
    GetTree().ChangeSceneToFile(levelPath);
    Animation.Play("FadeOut");
  }
}
