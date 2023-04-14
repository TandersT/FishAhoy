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
        Global.PossibleUpgrades = new System.Collections.Generic.List<UpgradesEnum>
        {
            UpgradesEnum.BiggerMouth,
            UpgradesEnum.BiggerMouth,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.FasterSwimSpeed,
            UpgradesEnum.FasterBeerhand,
            UpgradesEnum.FasterBeerhand,
            UpgradesEnum.RareRedDrops,
            UpgradesEnum.RareGoldBards,
            UpgradesEnum.MustacheAsCup,
            UpgradesEnum.LessReputationLoss,
            UpgradesEnum.IWillTellYouAboutMyFish,
            UpgradesEnum.INeedMoreAlcocol,
        };
    }
}

