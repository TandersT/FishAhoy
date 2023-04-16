using Godot;
using System;

public partial class FishVariation : Node2D
{
	AnimationPlayer AnimationPlayer;
	public override void _Ready()
	{
		AnimationPlayer = GetNode<AnimationPlayer>(nameof(AnimationPlayer));
		AnimationPlayer.SpeedScale = (float)GD.RandRange(0.5f,1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
