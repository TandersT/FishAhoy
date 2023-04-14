using Godot;
using System;

public partial class TiltBeer : AnimatableBody2D
{
    PackedScene PackedBeerParticle = GD.Load<PackedScene>("res://Entities/Beer/BeerParticle.tscn");
    Marker2D BeerParticleSpawn;
    public override void _Ready()
    {
        BeerParticleSpawn = GetNode<Marker2D>(nameof(BeerParticleSpawn));

        var tiltBeerTween = CreateTween();
        tiltBeerTween.TweenProperty(this, "rotation", Mathf.DegToRad(-120), 1).SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Sine);
        tiltBeerTween.Finished += StartBeerSpawn;
    }

    void StartBeerSpawn()
    {
        Timer spawnBeerTimer = new Timer();
        spawnBeerTimer.OneShot = false;
        spawnBeerTimer.WaitTime = 0.1f;
        AddChild(spawnBeerTimer);
        spawnBeerTimer.Timeout += SpawnBeerParticle;
        spawnBeerTimer.Start();
    }

    private void SpawnBeerParticle()
    {
        var _beerParticle = (BeerParticle)PackedBeerParticle.Instantiate();

        _beerParticle.TopLevel = true;
        _beerParticle.GlobalPosition = BeerParticleSpawn.GlobalPosition;
        BeerParticleSpawn.AddChild(_beerParticle);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        GlobalPosition = (GetViewport().GetMousePosition() * 0.5f + GlobalPosition * 0.5f) with { Y = 200 };
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouse mouseMotion)
        {
        }
    }
}
