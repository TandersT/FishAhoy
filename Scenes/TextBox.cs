using Godot;
using System;

public partial class TextBox : HBoxContainer
{
    string[] texts = {
        "Wowiee wowow ow ow wooow",
        "hibba habba duppa dappa",
        "lolo drunk fish hihi",
    };
    int textIndex = 0;
    Label TextLabel;
    public override void _Ready()
    {
        Global.TextBox = this;
        TextLabel = GetNode<Label>($"%{nameof(TextLabel)}");
        Timer NewTextTimer = new Timer();
        NewTextTimer.OneShot = false;
        NewTextTimer.WaitTime = 5;
        AddChild(NewTextTimer);
        NewTextTimer.Start();
        NewTextTimer.Timeout += SetNewText;
    }

    private void SetNewText()
    {
        NewText(texts[textIndex++ % texts.Length]);
    }

    public void NewText(string text)
    {
        TextLabel.VisibleRatio = 0;
        TextLabel.Text = text;
        var showText = CreateTween();
        showText.TweenProperty(TextLabel, "visible_ratio", 1, text.Length / 10f);

    }
}
