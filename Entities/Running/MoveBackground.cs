using Godot;
using System;

public partial class MoveBackground : Control
{
    float size;
    TextureRect BackgroundA, BackgroundB;
    public override void _Ready()
    {
        BackgroundA = (TextureRect)GetChild(0);
        BackgroundB = (TextureRect)BackgroundA.Duplicate();
        AddChild(BackgroundB);
        size = BackgroundB.Size.X;
        BackgroundB.Position = Vector2.Right * size;
    }

    public override void _Process(double delta)
    {
        this.Position += Vector2.Left * (float)delta * 500;
        if (BackgroundA.GlobalPosition.X < -size)
        {
            BackgroundA.GlobalPosition = BackgroundB.GlobalPosition + Vector2.Right * size;
        }
        if (BackgroundB.GlobalPosition.X < -size)
        {
            BackgroundB.GlobalPosition = BackgroundA.GlobalPosition + Vector2.Right * size;
        }
    }
}
