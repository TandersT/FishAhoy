using Godot;
using System;
using System.Collections.Generic;
using System.Text;



public enum MusicStateEnum
{
    Main,
    Drinking,
    Running,
    GameOver,
}

public class BaseData
{
    public float Duration = 10;
    public float DrunkenIntensity = 0.1f;
    public float DrunkenDuration = 2;

    // public BaseData(float drunkenIntensity, float drunkenDuration)
    // {
    //     DrunkenIntensity = drunkenIntensity;
    //     DrunkenDuration = drunkenDuration;
    // }
}

public enum GameStateEnum
{
    StartMenu,
    Cutscene,
    Running,
    Drinking,
}

public class RunningData : BaseData
{
    public float GameSpeed = 1;
    public float SwayDistance = 0;
    public float BadStuffChange = 0.33f;
    public int ReputationLost = -10; //The rep loss on bad stuff hit


    //     public RunningData(float gameSpeed, float swayDistance, float badStuffChange, float badStuffEffect, float drunkenIntensity, float drunkenDuration) : base(drunkenIntensity, drunkenDuration)
    // {
    //     GameSpeed = gameSpeed;
    //     SwayDistance = swayDistance;
    //     BadStuffChange = badStuffChange;
    //     BadStuffEffect = badStuffEffect;
    // }
}
public class DrinkingData : BaseData
{
    public float TargetScore = 25;
    public float MouthSpeed = 1;
    public float DropSize = 1;
    public Texture2D DropTexture;
    public float DropSpawnScalar = 1;
    public float DropWeightScalar = 1;
    public float HandSway = 0;
    public bool FinItnerference = false; // A find swinning from the side to dash away the drops
    public float Handeling = 0.95f;

    // public DrinkingData(float mouthSpeed, float dropSize, Texture2D dropTexture, float dropSpawScalar, float dropWeightScalar, float handSway, float chanceOfFinInterfernce, float drunkenIntensity, float drunkenDuration) : base(drunkenIntensity, drunkenDuration)
    // {
    //     MouthSpeed = mouthSpeed;
    //     DropSize = dropSize;
    //     DropTexture = dropTexture;
    //     DropSpawScalar = dropSpawScalar;
    //     DropWeightScalar = dropWeightScalar;
    //     HandSway = handSway;
    //     ChanceOfFinInterfernce = chanceOfFinInterfernce;
    // }
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
    public static bool GodMode = false;
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
    public static string DrinkingScene { get; internal set; } = "res://Scenes/Drinking.tscn";
    public static string RunningScene { get; internal set; } = "res://Scenes/Running.tscn";
    public static VisualNovelTypeEnum VisualNovelType = VisualNovelTypeEnum.End;
    public static string VisuelNovel { get; internal set; } = "res://Scenes/VisuelNovel.tscn";
    public static string GameOver { get; internal set; } = "res://Scenes/GameOver.tscn";

    public static FadeInOut FadeInOut { get; internal set; }
    public static Guide Guide { get; internal set; }
    public static string Main { get; internal set; } = "res://Scenes/Main.tscn";
    public static string NewBar { get; internal set; } = "res://Scenes/NewBar.tscn";
    public static string YouWin { get; internal set; } = "res://Scenes/NewBar.tscn";

    public static int CurrentLevel = 0;
    public static string[] BarNames =
    {
        "The Salty Swimmer",
        "The Tuna Taproom",
        "The Mackerel's Mermaid",
        "Land Ahoy"
    };
    public static DrinkingData[] DrinkingLevels =
    {
        new DrinkingData()
        {
            Duration = 60,
            TargetScore = 80,
            DrunkenIntensity = 0.1f,
            DrunkenDuration = 2,
            MouthSpeed = 0.5f,
            DropSize = 1,
            DropTexture = null,
            DropSpawnScalar = .7f,
            DropWeightScalar = 3,
            HandSway = 2,
            FinItnerference = false,
            Handeling = 0.9f,
        }, //1
        new DrinkingData()
        {
            Duration = 25,
            TargetScore = 40,
            DrunkenIntensity = 0.1f,
            DrunkenDuration = 2,
            MouthSpeed = 0.6f,
            DropSize = 1,
            DropTexture = null,
            DropSpawnScalar = .7f,
            DropWeightScalar = 5,
            HandSway = 3,
            FinItnerference = false,
            Handeling = 0.95f,
        },  //2
        new DrinkingData()
        {
            Duration = 25,
            TargetScore = 60,
            DrunkenIntensity = 0.1f,
            DrunkenDuration = 2,
            MouthSpeed = 0.7f,
            DropSize = 1f,
            DropTexture = null,
            DropSpawnScalar = .8f,
            DropWeightScalar = 10,
            HandSway = 20,
            FinItnerference = true,
            Handeling = 0.99f,
        }, //6 
        new DrinkingData()
        {
            Duration = 50,
            TargetScore = 175,
            DrunkenIntensity = 0.1f,
            DrunkenDuration = 2,
            MouthSpeed = 0.9f,
            DropSize = 0.5f,
            DropTexture = null,
            DropSpawnScalar = 1.2f,
            DropWeightScalar = 3,
            HandSway = 25,
            FinItnerference = true,
            Handeling = 0.99f,
        },  //7
    };
    public static RunningData[] RunningLevels =
    {
        new RunningData()
        {
            Duration = 15,
            DrunkenIntensity = 0.1f,
            DrunkenDuration = 2,
            GameSpeed = 1,
            SwayDistance = 0,
            BadStuffChange = 0.33f,
            ReputationLost = -25,
        }, //1
        new RunningData()
        {
            Duration = 15,
            DrunkenIntensity = 0.4f,
            DrunkenDuration = 4,
            GameSpeed = 1.5f,
            SwayDistance = .5f,
            BadStuffChange = 0.4f,
            ReputationLost = -45,
        }, //2
        new RunningData()
        {
            Duration = 15,
            DrunkenIntensity = 0.4f,
            DrunkenDuration = 4,
            GameSpeed = 1.7f,
            SwayDistance = 1f,
            BadStuffChange = 0.45f,
            ReputationLost = -50,
        }, //3
    //    new RunningData()
    //     {
    //         Duration = 15,
    //         DrunkenIntensity = 0.4f,
    //         DrunkenDuration = 4,
    //         GameSpeed = 1.5f,
    //         SwayDistance = 2f,
    //         BadStuffChange = 0.45f,
    //         ReputationLost = -35,
    //     }, //4
        new RunningData()
        {
            Duration = 15,
            DrunkenIntensity = 0.4f,
            DrunkenDuration = 4,
            GameSpeed = 2.2f,
            SwayDistance = 4f,
            BadStuffChange = 0.45f,
            ReputationLost = -60,
        },  //5
    };

    public static bool GuidePause, FadePause, CardPause;
    public static void Unpause()
    {
        if (!GuidePause && !FadePause && !CardPause)
        {
            Guide.GetTree().Paused = false;
        }
    }
    public static List<UpgradesEnum> PossibleDrinkingUpgrades;
    public static List<UpgradesEnum> PossibleRunningUpgrades;
    public static string LatestState;
    public static MusicStateEnum LatestMusicState;
    public static void CustomChangeScene(string scene, MusicStateEnum musicStateEnum)
    {
        if (scene != GameOver)
        {
            LatestState = scene;
            LatestMusicState = musicStateEnum;
        }
        FadeInOut.StartFade(scene, musicStateEnum);
    }
    public static float SwimSpeed = 1;

    public static float MouthSize = 0.76f;
    public static bool EnableWhiskers = false;
    public static float BeerHandScalar = 1;
    public static int ReputationlossReducation = 0;
    public static float AlcoholIncrease = 0;
    public static void AddUpgrade(UpgradesEnum upgrade)
    {
        switch (upgrade)
        {
            case UpgradesEnum.BiggerMouth:
                MouthSize += 0.04f;
                break;
            case UpgradesEnum.FasterSwimSpeed:
                SwimSpeed = SwimSpeed * 0.8f;
                break;
            case UpgradesEnum.FasterBeerhand:
                BeerHandScalar *= 1.3f;
                break;
            // case UpgradesEnum.RareRedDrops:
            //     break;
            // case UpgradesEnum.RareGoldBards:
            //     break;
            case UpgradesEnum.MustacheAsCup:
                EnableWhiskers = true;
                break;
            case UpgradesEnum.LessReputationLoss:
                ReputationlossReducation += 5;
                break;
            // case UpgradesEnum.IWillTellYouAboutMyFish:
            //     break;
            case UpgradesEnum.INeedMoreAlcocol:
                AlcoholIncrease += 5;
                break;
        }
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
