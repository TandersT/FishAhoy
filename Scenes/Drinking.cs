using Godot;
using System;
using System.Diagnostics;

public partial class Drinking : Control
{
    bool gameEnded = false;
    float ShownScore;
    float ActualScore;
    Label ScoreValue, TargetValue, DurationLeft;
    Area2D MouthArea;
    Stopwatch stopwatch = new Stopwatch();

    CollisionShape2D Whisker1, Whisker2;

    TiltBeer Beer;
    public DrinkingData Data;

    AnimationPlayer MouthAnimation;
    AnimationTree FinAnimation;
    HBoxContainer CardContainer;
    PackedScene PackedCard = GD.Load<PackedScene>("res://Scenes/Card.tscn");
    Sprite2D Mouth, BigBeard;
    AnimatableBody2D MouthCollider;
    Node2D MouthPivot;
    public void SetLevelDifficulty(DrinkingData data)
    {
        Data = data;

        TargetValue.Text = (data.TargetScore + Global.AlcoholIncrease).ToString(); ;
        DurationLeft.Text = data.Duration.ToString();
        MouthAnimation.SpeedScale = data.MouthSpeed;
        FinAnimation.Active = data.FinItnerference;

        MouthCollider.Scale = Global.MouthSize * Vector2.One;
        MouthPivot.Scale = Global.MouthSize * Vector2.One;

        Whisker1.Disabled = !Global.EnableWhiskers;
        Whisker2.Disabled = !Global.EnableWhiskers;
        BigBeard.Visible = Global.EnableWhiskers;
    }

    public override void _EnterTree()
    {
        ScoreValue = GetNode<Label>($"%{nameof(ScoreValue)}");
        ScoreValue.Text = 0.ToString();
        TargetValue = GetNode<Label>($"%{nameof(TargetValue)}");
        DurationLeft = GetNode<Label>($"%{nameof(DurationLeft)}");
        MouthArea = GetNode<Area2D>($"%{nameof(MouthArea)}");
        MouthAnimation = GetNode<AnimationPlayer>($"%{nameof(MouthAnimation)}");
        CardContainer = GetNode<HBoxContainer>($"%{nameof(CardContainer)}");
        FinAnimation = GetNode<AnimationTree>($"%{nameof(FinAnimation)}");
        Beer = GetNode<TiltBeer>($"%{nameof(Beer)}");
        MouthCollider = GetNode<AnimatableBody2D>($"%{nameof(MouthCollider)}");
        MouthPivot = GetNode<Node2D>($"%{nameof(MouthPivot)}");

        Whisker1 = GetNode<CollisionShape2D>($"%{nameof(Whisker1)}");
        Whisker2 = GetNode<CollisionShape2D>($"%{nameof(Whisker2)}");
        BigBeard = GetNode<Sprite2D>($"%{nameof(BigBeard)}");

        SetLevelDifficulty(Global.DrinkingLevels[Global.CurrentLevel]);
    }
    public override void _Ready()
    {
        MouthArea.BodyEntered += UpdateScore;
        if (Global.CurrentLevel == 0)
        {
            Global.Guide.ShowGuide("You control the hand with A and D.\nGet as many drops of alcohol inside the mouth before the times runs out.\nIf you reach the targeted dl you win!");
        }
    }

    internal void StartGame()
    {
        stopwatch.Start();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        ShownScore = ShownScore * 0.95f + ActualScore * 0.05f;
        ScoreValue.Text = ShownScore.ToString("F");
        var timeLeft = Data.Duration - stopwatch.Elapsed.TotalSeconds;
        DurationLeft.Text = timeLeft.ToString("F");
        if (timeLeft < 0 && !gameEnded && !Global.GodMode)
        {
            GD.Print("Game Ended");
            Global.CustomChangeScene(Global.GameOver, MusicStateEnum.GameOver);
            gameEnded = true;
        }
        if (ShownScore > Data.TargetScore + Global.AlcoholIncrease && !gameEnded)
        {
            stopwatch.Stop();
            GD.Print("You win!");
            if (Global.CurrentLevel == 3)
            {
                Global.VisualNovelType = VisualNovelTypeEnum.End;
                Global.CustomChangeScene(Global.VisuelNovel, MusicStateEnum.Main);
                return;
            }
            SpawnCards();
            Global.CurrentLevel++;
            gameEnded = true;
        }
    }

    public void SpawnCards()
    {

        if (Global.CurrentLevel == 0)
        {
            Global.Guide.ShowGuide("After each drinking round you get\na chance to upgrade some of your abilities.\nChoose wisely!");
        }
        var a = Global.PossibleDrinkingUpgrades[(int)(GD.Randi() % Global.PossibleDrinkingUpgrades.Count)];
        Global.PossibleDrinkingUpgrades.Remove(a);
        UpgradesEnum b;
        do
        {
            b = Global.PossibleDrinkingUpgrades[(int)(GD.Randi() % Global.PossibleDrinkingUpgrades.Count)];

        } while (b == a);
        Global.PossibleDrinkingUpgrades.Remove(b);
        var _cardA = (Card)PackedCard.Instantiate();
        _cardA.music = MusicStateEnum.Running;
        _cardA.scene = Global.RunningScene;
        _cardA.Upgrade = a;
        var _cardB = (Card)PackedCard.Instantiate();
        _cardB.music = MusicStateEnum.Running;
        _cardB.scene = Global.RunningScene;
        _cardB.Upgrade = b;
        CardContainer.AddChild(_cardA);
        CardContainer.AddChild(_cardB);
        Global.CardPause = true;
        GetTree().Paused = true;
    }

    private void UpdateScore(Node2D area)
    {
        if (area is BeerParticle beer)
        {
            if (!beer.isUsed)
            {
                ActualScore++;
                beer.isUsed = true;
                beer.QueueFree();
            }
        }
    }
}
