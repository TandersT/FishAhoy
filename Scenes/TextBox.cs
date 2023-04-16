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
    TextureRect Image;
    Vector2 In = new Vector2(325, 830);
    Vector2 Out = new Vector2(325, 1100);
    public override void _Ready()
    {
        Global.TextBox = this;
        Image = GetNode<TextureRect>($"%{nameof(Image)}");
        TextLabel = GetNode<Label>($"%{nameof(TextLabel)}");
        Position = Out;
        // Timer NewTextTimer = new Timer();
        // NewTextTimer.OneShot = false;
        // NewTextTimer.WaitTime = 5;
        // AddChild(NewTextTimer);
        // NewTextTimer.Start();
        // NewTextTimer.Timeout += SetNewText;
    }

    private void SetNewText()
    {
        // NewText(texts[textIndex++ % texts.Length]);
    }

    public Tween EnterTextBox()
    {
        var showText = CreateTween();
        showText.TweenProperty(this, "position", In, 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
        return showText;
    }

    public Tween NewText(NovelPoints character, string text)
    {
        // Set Image
        TextLabel.VisibleRatio = 0;
        TextLabel.Text = text;
        var showText = CreateTween();
        showText.TweenProperty(TextLabel, "visible_ratio", 1, text.Length / 25f);
        if (character != null)
        {
            Image.Modulate = character.Modulate;
            character.isTalking = true;
            character.AudioTalk();
            showText.Finished += () => EndTalk(character);
        }
        else
        {
            Image.Modulate = Colors.Transparent;
        }
        return showText;
    }

    private void EndTalk(NovelPoints character)
    {
        character.isTalking = false;
    }
}
