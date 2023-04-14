using Godot;
using System;
public partial class Card : PanelContainer
{
    public UpgradesEnum Upgrade = UpgradesEnum.IWillTellYouAboutMyFish;

    Label Label;
    TextureButton Button;
    public override void _Ready()
    {
        base._Ready();
        Label = GetNode<Label>($"%{nameof(Label)}");
        Button = GetNode<TextureButton>($"%{nameof(Button)}");

        Button.Pressed += () => Global.AddUpgrade(Upgrade);
		Label.Text = Global.AddSpacesToSentence(Upgrade.ToString());
    }

}
