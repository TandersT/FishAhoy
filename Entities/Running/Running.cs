using Godot;
using System;


public partial class Running : Control
{

    bool isMoving = false;
    float MoveHeight = 200;
    float MoveDuration = 1.5f;
    Sprite2D Fish;
    public override void _Ready()
    {
        Fish = GetNode<Sprite2D>($"%{nameof(Fish)}");
    }

    public override void _Process(double delta)
    {
    }
    public void SetLevelDifficulty(RunningData data)
    {

    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventKey key)
        {
            if (key.Pressed && !key.IsEcho() && !isMoving)
            {
                if (key.Keycode == Key.W)
                {
                    isMoving = true;
                    var moveTween = CreateTween();
                    moveTween.TweenMethod(new Callable(this, nameof(MoveUp)), 0f, Mathf.Pi, MoveDuration * Global.SwimSpeed).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
                    moveTween.Finished += ResetPosition;
                }
                if (key.Keycode == Key.S)
                {
                    isMoving = true;
                    var moveTween = CreateTween();
                    moveTween.TweenMethod(new Callable(this, nameof(MoveDown)), 0f, Mathf.Pi, MoveDuration * Global.SwimSpeed).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
                    moveTween.Finished += ResetPosition;
                }
            }
        }
    }

    public void ResetPosition()
    {
        isMoving = false;
        Fish.Position = Vector2.Zero;
    }
    void MoveUp(float delta)
    {
        var pos = Vector2.Up * Mathf.Sin(delta) * MoveHeight;
        Fish.Position = pos;
    }
    void MoveDown(float delta)
    {
        var pos = Vector2.Down * Mathf.Sin(delta) * MoveHeight;
        Fish.Position = pos;
    }
}
