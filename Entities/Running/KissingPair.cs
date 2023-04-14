using Godot;
using System;

public partial class KissingPair : CollidableSprite
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

    protected override void OnAreaEntered(Area2D area)
    {
		QueueFree();
    }
}
