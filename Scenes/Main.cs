using Godot;
using System;

public partial class Main : Control
{
    Button Start, Quit;
    public override void _Ready()
    {
        Start = GetNode<Button>($"%{nameof(Start)}");
        Quit = GetNode<Button>($"%{nameof(Quit)}");

        Start.Pressed += OnStartGame;
        Quit.Pressed += () => GetTree().Quit();
    }

    private void OnStartGame()
    {
        Global.stopwatch.Restart();
        Global.CurrentLevel = 0;
        Global.PossibleDrinkingUpgrades = new System.Collections.Generic.List<UpgradesEnum>
        {
            UpgradesEnum.BiggerMouth,
            UpgradesEnum.BiggerMouth,
            UpgradesEnum.BiggerMouth,
            UpgradesEnum.FasterBeerhand,
            UpgradesEnum.FasterBeerhand,
            UpgradesEnum.FasterBeerhand,
            // UpgradesEnum.RareRedDrops,
            // UpgradesEnum.RareGoldBards,
            // UpgradesEnum.MustacheAsCup,
            // UpgradesEnum.IWillTellYouAboutMyFish,
            UpgradesEnum.INeedMoreAlcocol,
            UpgradesEnum.INeedMoreAlcocol,
        };
        Global.PossibleRunningUpgrades = new System.Collections.Generic.List<UpgradesEnum>
        {
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.LessReputationLoss,
            UpgradesEnum.LessReputationLoss,
            UpgradesEnum.LessReputationLoss,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.LessReputationLoss,
            UpgradesEnum.LessReputationLoss,
            UpgradesEnum.LessReputationLoss,
            // UpgradesEnum.RareRedDrops,
            // UpgradesEnum.RareGoldBards,
            // UpgradesEnum.IWillTellYouAboutMyFish,
            UpgradesEnum.INeedMoreAlcocol,
            UpgradesEnum.INeedMoreAlcocol,
        };

        Global.MouthSize = 0.7f;
        Global.SwimSpeed = 1;
        Global.BeerHandScalar = 1;
        Global.ReputationlossReducation = 0;

        Global.VisualNovelType = VisualNovelTypeEnum.Start;
        Global.CustomChangeScene(Global.VisuelNovel, MusicStateEnum.Main);
    }
}

