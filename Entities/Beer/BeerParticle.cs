using Godot;
using System;

public partial class BeerParticle : RigidBody2D
{
	public Sprite2D sprite;
	public bool isUsed = false;
    public override void _Ready()
    {
        base._Ready();
		sprite = GetNode<Sprite2D>("Beer");
		VisibleOnScreenNotifier2D visibilityNotifier = new();
		AddChild(visibilityNotifier);
		visibilityNotifier.ScreenExited += QueueFree;
    }
    // public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    // {
    //     base._IntegrateForces(state);
	// 	if (!setup)
	// 	{
	// 		var posTrans = new Transform2D();
	// 		// posTrans.Origin = StartPosition;
	// 		// state.Transform = posTrans;
	// 		setup = true;
	// 	}
    // }
   
}
