using Godot;
using System;


public partial class Running : Control
{
    int Reputation, Coins;
    HSlider ReputationSlider;
    Label CoinsValue;
    SpawnObejcts SpawnObjects;
    TextureRect TopBarBG;

    bool isMoving = false;
    float MoveHeight = 200;
    float MoveDuration = 1.5f;
    Sprite2D Fish;

    HBoxContainer CardContainer;
    PackedScene PackedCard = GD.Load<PackedScene>("res://Scenes/Card.tscn");
    public RunningData Data { get; internal set; }

    public override void _Ready()
    {
        Fish = GetNode<Sprite2D>($"%{nameof(Fish)}");
        SpawnObjects = GetNode<SpawnObejcts>($"%{nameof(SpawnObjects)}");
        ReputationSlider = GetNode<HSlider>($"%{nameof(ReputationSlider)}");
        TopBarBG = GetNode<TextureRect>($"%{nameof(TopBarBG)}");
        CardContainer = GetNode<HBoxContainer>($"%{nameof(CardContainer)}");

        SetLevelDifficulty(Global.RunningLevels[Global.CurrentLevel]);

        UpdateReputation(100);

        if (Global.CurrentLevel == 0)
        {
            Global.Guide.ShowGuide("You control swimming up and down with W and S.\nKeep your reputation up by avoiding the snakes.\nGain extra street cred by hitting the coins!\nIf your reputation gets too low, they wont admit you into the next bar and you lose!");
        }
        if (Global.CurrentLevel == 1)
        {
            Global.Guide.ShowGuide("Keep it up!\nYou have to advance through all 7 bars, to reach Land Ahoy.\n\n Good luck!");
        }
    }

    public void SpawnCards()
    {

        if (Global.CurrentLevel == 0)
        {
            Global.Guide.ShowGuide("After each running round you get\na chance to upgrade some of your running abilities.\nChoose wisely!");
        }
        var a = Global.PossibleRunningUpgrades[(int)(GD.Randi() % Global.PossibleRunningUpgrades.Count)];
        Global.PossibleRunningUpgrades.Remove(a);
        UpgradesEnum b;
        do
        {
            b = Global.PossibleRunningUpgrades[(int)(GD.Randi() % Global.PossibleRunningUpgrades.Count)];

        } while (b == a);
        Global.PossibleRunningUpgrades.Remove(b);
        var _cardA = (Card)PackedCard.Instantiate();

        var state = Global.NewBar;
        var music = MusicStateEnum.Running;
        if (Global.CurrentLevel == 2)
        {
            state = Global.Midway;
            music = MusicStateEnum.Main;
        }
        _cardA.Upgrade = a;
        _cardA.music = music;
        _cardA.scene = state;
        var _cardB = (Card)PackedCard.Instantiate();
        _cardB.music = music;
        _cardB.scene = state;
        _cardB.Upgrade = b;
        CardContainer.AddChild(_cardA);
        CardContainer.AddChild(_cardB);
        Global.CardPause = true;
        GetTree().Paused = true;
    }
    Vector2 velocity;
    public override void _Process(double delta)
    {

        velocity = Fish.Position * 0.1f + velocity * 0.9f;
        Fish.RotationDegrees = velocity.Y * 0.1f;
    }
    public void SetLevelDifficulty(RunningData data)
    {
        Data = data;
        Tween DurationTween = CreateTween();
        DurationTween.TweenProperty(TopBarBG, "position:x", -500, data.Duration);
        DurationTween.Finished += EndRunning;
    }

    private void EndRunning()
    {
        GD.Print("You win!");
        SpawnCards();
    }


    public void UpdateReputation(int added)
    {
        Reputation += added;
        ReputationSlider.Value = Reputation;
        GD.Print(Reputation);
        if (Reputation <= 0 && !Global.GodMode)
        {
            GD.Print("You lost!");
            Global.CustomChangeScene(Global.GameOver, MusicStateEnum.GameOver);
        }
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
                    velocity = Vector2.Up * MoveHeight / 2;
                }
                if (key.Keycode == Key.S)
                {
                    isMoving = true;
                    var moveTween = CreateTween();
                    moveTween.TweenMethod(new Callable(this, nameof(MoveDown)), 0f, Mathf.Pi, MoveDuration * Global.SwimSpeed).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
                    moveTween.Finished += ResetPosition;
                    velocity = Vector2.Down * MoveHeight / 2;
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
