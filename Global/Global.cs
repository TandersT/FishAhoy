using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public enum GameStateEnum
{
    StartMenu,
    Cutscene,
    Running,
    Drinking,
}

public class RunningData
{

}
public class DrinkingData
{

}

public enum UpgradesEnum
{
    BiggerMouth,
    FasterSwimSpeed,
    FasterBeerhand,
    RareRedDrops,
    RareGoldBards,
    MustacheAsCup,
    LessReputationLoss,
    IWillTellYouAboutMyFish,
    INeedMoreAlcocol

}

public static class Global
{
    public delegate void GameStateChangedDelegate(GameStateEnum gameState);
    public static event GameStateChangedDelegate OnGameStateChangedDelegate;
    private static GameStateEnum gameState = GameStateEnum.StartMenu;
    public static GameStateEnum GameState
    {
        get { return gameState; }
        set
        {
            gameState = value;
            OnGameStateChangedDelegate?.Invoke(value);
        }
    }


    public static TextBox TextBox { get; internal set; }
    public static float SwimSpeed = 1;

    public static int CurrentLevel = 0;
    public static DrinkingData[] DrinkingLevels =
    {
        new DrinkingData(), //1
        new DrinkingData(), //2
        new DrinkingData(), //3
        new DrinkingData(), //4
        new DrinkingData(), //5
        new DrinkingData(), //6 
        new DrinkingData(), //7
    };
    public static RunningData[] RunningLevels =
{
        new RunningData(), //1
        new RunningData(), //2
        new RunningData(), //3
        new RunningData(), //4
        new RunningData(), //5
        new RunningData(), //6
    };



    public static List<UpgradesEnum> PossibleUpgrades;

    public static void AddUpgrade(UpgradesEnum upgrade)
    {
        switch (upgrade)
        {
            case UpgradesEnum.BiggerMouth:
                break;
            case UpgradesEnum.FasterSwimSpeed:
                SwimSpeed = SwimSpeed * 0.8f;
                break;
            case UpgradesEnum.FasterBeerhand:
                break;
            case UpgradesEnum.RareRedDrops:
                break;
            case UpgradesEnum.RareGoldBards:
                break;
            case UpgradesEnum.MustacheAsCup:
                break;
            case UpgradesEnum.LessReputationLoss:
                break;
            case UpgradesEnum.IWillTellYouAboutMyFish:
                break;
            case UpgradesEnum.INeedMoreAlcocol:
                break;
        }
        PossibleUpgrades.Remove(upgrade);
    }

    public static string AddSpacesToSentence(string text, bool preserveAcronyms = false)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.
            IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
}
