using Godot;
using System;

public partial class SpawnObejcts : Node2D
{
    PackedScene PackedCoin = GD.Load<PackedScene>("res://Entities/Running/Objects/Coin.tscn");
    PackedScene PackedKissingPair = GD.Load<PackedScene>("res://Entities/Running/Objects/KissingPair.tscn");

    Timer AddObjectTimer = new Timer();

    float[] ObjectPositions = {
        300,
        500,
        700,
    };
    public override void _Ready()
    {
        AddObjectTimer.WaitTime = 1;
        AddObjectTimer.OneShot = true;
        AddChild(AddObjectTimer);
        AddObjectTimer.Timeout += SpawnObject;
        AddObjectTimer.Start();
    }

    private void SpawnObject()
    {
        var rand = GD.Randf();
        PackedScene _packedObject;
        if (rand < 0.5f)
        {
            _packedObject = PackedCoin;
        }
        else
        {
            _packedObject = PackedKissingPair;
        }
        var _object = (CollidableSprite)_packedObject.Instantiate();
        AddChild(_object);
        _object.GlobalPosition = new Vector2(2200, ObjectPositions[GD.Randi() % 3]);
        AddObjectTimer.WaitTime = GD.RandRange(2,2.5);
        AddObjectTimer.Start();
    }

    public override void _Process(double delta)
    {
        this.Position += Vector2.Left * (float)delta * 750;
    }
}
