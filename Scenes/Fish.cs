using Godot;
using System;

public partial class Fish : Sprite2D
{
    Running Running;
    Area2D Area2D;
    public override void _Ready()
    {
        Running = (Running)GetParent().GetParent();
        Area2D = GetNode<Area2D>(nameof(Area2D));
        Area2D.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area.GetParent() is Coin coin)
        {
            Running.UpdateReputation(5);
            coin.QueueFree();
        }
        if (area.GetParent() is KissingPair kissingPair)
        {
            Running.UpdateReputation(Running.Data.ReputationLost + Global.ReputationlossReducation);
            kissingPair.QueueFree();
        }
    }
    public override void _Process(double delta)
    {

    }
}
