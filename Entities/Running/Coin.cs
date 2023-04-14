using Godot;
using System;
public abstract partial class CollidableSprite : Sprite2D
{
    Area2D Area2D;
    public override void _Ready()
    {
        base._Ready();
        Area2D = GetNode<Area2D>(nameof(Area2D));
        Area2D.AreaEntered += OnAreaEntered;
		VisibleOnScreenNotifier2D leftScreen = new VisibleOnScreenNotifier2D();
		AddChild(leftScreen);
		leftScreen.ScreenExited += QueueFree; 
    }

    protected abstract void OnAreaEntered(Area2D area);
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

    protected override void OnAreaEntered(Area2D area)
    {
        QueueFree();
    }
}
