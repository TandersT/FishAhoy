using Godot;
using System;

public partial class SpawnObejcts : Node2D
{
    PackedScene PackedCoin = GD.Load<PackedScene>("res://Entities/Running/Objects/Coin.tscn");
    PackedScene PackedKissingPair = GD.Load<PackedScene>("res://Entities/Running/Objects/KissingPair.tscn");

    Timer AddObjectTimer = new Timer();
    Running Running;
    float[] ObjectPositions = {
        300,
        500,
        700,
    };
    public override void _Ready()
    {
        Running = (Running)GetParent();


        AddObjectTimer.WaitTime = 1;
        AddObjectTimer.OneShot = true;
        AddChild(AddObjectTimer);
        AddObjectTimer.Timeout += SpawnObject;
        AddObjectTimer.Start();

    }

    private void SpawnObject()
    {
        var rand = GD.Randf();
        uint objects = GD.Randi() % 2 + 1;
        uint firstPos = 0;
        for (int i = 0; i < objects; i++)
        {
            PackedScene _packedObject;
            if (rand < Running.Data.BadStuffChange)
            {
                //spawn good stuff
                _packedObject = PackedCoin;
            }
            else
            {
                //spawn bad stuff
                _packedObject = PackedKissingPair;
            }
            var _object = (CollidableSprite)_packedObject.Instantiate();
            AddChild(_object);
            uint pos = 0;
            do
            {
                pos = GD.Randi() % 3;
            } while (firstPos == pos);
            _object.GlobalPosition = new Vector2(2200, ObjectPositions[pos]);
            firstPos = pos;
        }
        float baseWait = 1.2f;
        if (Global.CurrentLevel >= 3)
        {
            baseWait = 1f;
        }
        if (Global.CurrentLevel >= 5)
        {
            baseWait = 0.8f;
        }
        AddObjectTimer.WaitTime = GD.RandRange(baseWait, baseWait + 0.5f);
        AddObjectTimer.Start();
    }

    public override void _Process(double delta)
    {
        this.Position += Vector2.Left * (float)delta * 1250 * Running.Data.GameSpeed;
    }
}
