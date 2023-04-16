using Godot;
using System;

public partial class TiltBeer : AnimatableBody2D
{
    PackedScene PackedBeerParticle = GD.Load<PackedScene>("res://Entities/Beer/BeerParticle.tscn");
    Marker2D BeerParticleSpawn;
    Drinking Drinking;
    Node2D BeerParticles;
    Vector2 Velocity;
    float walkSpeed = 1000;
    public override void _Ready()
    {
        BeerParticles = GetNode<Node2D>($"%{nameof(BeerParticles)}");
        BeerParticleSpawn = GetNode<Marker2D>(nameof(BeerParticleSpawn));
        Drinking = (Drinking)GetParent();
        // var tiltBeerTween = CreateTween();
        // tiltBeerTween.TweenProperty(this, "rotation", Mathf.DegToRad(-120), 1).SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Sine);
        // tiltBeerTween.Finished += StartBeerSpawn;
        StartBeerSpawn();
    }

    void StartBeerSpawn()
    {
        Timer spawnBeerTimer = new Timer();
        spawnBeerTimer.OneShot = false;

        spawnBeerTimer.WaitTime = 0.1f / Drinking.Data.DropSpawnScalar;
        AddChild(spawnBeerTimer);
        spawnBeerTimer.Timeout += SpawnBeerParticle;
        spawnBeerTimer.Timeout += Drinking.StartGame;
        spawnBeerTimer.Start();
    }

    private void SpawnBeerParticle()
    {
        var _beerParticle = (BeerParticle)PackedBeerParticle.Instantiate();
        _beerParticle.GlobalPosition = BeerParticleSpawn.GlobalPosition;
        BeerParticles.AddChild(_beerParticle);
        _beerParticle.Mass = 1 * Drinking.Data.DropWeightScalar;
        _beerParticle.Scale = Drinking.Data.DropSpawnScalar * Vector2.One;
        // _beerParticle.sprite.Texture = Drinking.Data.DropTexture; // TODO: Add texture!
    }
    float totalDelta = 0;
    public override void _PhysicsProcess(double delta)
    {
        float floatDelta = (float)delta;
        totalDelta += floatDelta;
        if (Input.IsActionPressed("left"))
        {
            Velocity.X = -(walkSpeed * Global.BeerHandScalar) * 0.05f + Velocity.X * 0.95f;
        }
        else if (Input.IsActionPressed("right"))
        {
            Velocity.X = (walkSpeed * Global.BeerHandScalar) *0.05f + Velocity.X * 0.95f;
        }
        else
        {
            Velocity.X = Velocity.X * Drinking.Data.Handeling;
        }

        GlobalPosition += Velocity * floatDelta;
        RotationDegrees = Mathf.Sin(totalDelta) * Drinking.Data.HandSway;
    }
}
