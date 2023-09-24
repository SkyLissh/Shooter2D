using System;
using System.Collections.Generic;
using Godot;
using Shooter2D.Scripts.Player;

namespace Shooter2D.Scripts.Items;

public enum ItemOptions
{
  Laser,
  Grenade,
  Health
}

public partial class Item : Area2D
{
  [Export]
  public int RotationSpeed { get; set; } = 4;

  [Export]
  public Sprite2D Sprite { get; set; }

  private readonly List<ItemOptions> _options =
    new()
    {
      ItemOptions.Health,
      ItemOptions.Laser,
      ItemOptions.Laser,
      ItemOptions.Laser,
      ItemOptions.Grenade
    };

  private ItemOptions _itemOption;

  public override void _Ready()
  {
    _itemOption = _options[new Random().Next(_options.Count)];

    switch (_itemOption)
    {
      case ItemOptions.Laser:
        Sprite.Modulate = new Color("358af2");
        break;
      case ItemOptions.Grenade:
        Sprite.Modulate = new Color("f24e35");
        break;
      case ItemOptions.Health:
        Sprite.Modulate = new Color("aaf235");
        break;
    }
  }

  public override void _Process(double delta)
  {
    Rotation += RotationSpeed * (float)delta;
  }

  private void OnBodyEntered(Node2D body)
  {
    if (body is not PlayerController)
      return;

    (body as PlayerController).AddItem(_itemOption);
    QueueFree();
  }
}
