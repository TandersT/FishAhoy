using Godot;
using System;
public partial class Card : PanelContainer
{
    public UpgradesEnum Upgrade = UpgradesEnum.IWillTellYouAboutMyFish;
    public string scene;
    public MusicStateEnum music;
    Label Label;
    TextureButton Button;
    public override void _Ready()
    {
        base._Ready();

        Label = GetNode<Label>($"%{nameof(Label)}");
        Button = GetNode<TextureButton>($"%{nameof(Button)}");

        Button.Pressed += () => Global.AddUpgrade(Upgrade);
        Button.Pressed += ChangeScene;
        switch (Upgrade)
        {
            case UpgradesEnum.BiggerMouth:
                Label.Text = "My mouth will grow, allowing me to gobble more of this delicous beer";
                break;
            case UpgradesEnum.FasterBeerhand:
                Label.Text = "My arm grows strong, moving faster than before!";
                break;
            case UpgradesEnum.FasterSwimSpeed:
                Label.Text = "My fins grows more agile, allowing me to jump faster up and down in the water!";
                break;
            case UpgradesEnum.INeedMoreAlcocol:
                Label.Text = "My hungers grows making my desire for alcohol stronger! This is the best one, and you should always pick it, trust me";
                break;
            case UpgradesEnum.LessReputationLoss:
                Label.Text = "My street credz grows, and they will look the other way when I do bad stuff!";
                break;
            case UpgradesEnum.MustacheAsCup:
                Label.Text = "My mustache grows bigger, making it assist my quest to gobble beer!";
                break;
            default:
                Label.Text = Global.AddSpacesToSentence(Upgrade.ToString());
                break;

        }
    }

    private void ChangeScene()
    {
        Global.CardPause = false;
        Global.CustomChangeScene(scene, music);
    }
}
