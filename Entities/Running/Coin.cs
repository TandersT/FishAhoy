using Godot;
using System;
public abstract partial class CollidableSprite : Sprite2D
{
    Area2D Area2D;
    public override void _Ready()
    {
        base._Ready();
        Area2D = GetNode<Area2D>(nameof(Area2D));
		VisibleOnScreenNotifier2D leftScreen = new VisibleOnScreenNotifier2D();
		AddChild(leftScreen);
		leftScreen.ScreenExited += QueueFree; 
    }
}
public partial class Coin : CollidableSprite
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
    }
}
