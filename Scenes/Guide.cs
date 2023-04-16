using Godot;
using System;

public partial class Guide : CanvasLayer
{
    Label TextLabel;
    Button OkButton;
    public override void _Ready()
    {
        Global.Guide = this;
        TextLabel = GetNode<Label>($"%{nameof(TextLabel)}");
        OkButton = GetNode<Button>($"%{nameof(OkButton)}");
        Hide();

        OkButton.Pressed += Unpause;
    }

    private void Unpause()
    {
        Global.GuidePause = false;
        Global.Unpause();
        Hide();
    }

    public void ShowGuide(string text)
    {
        Global.GuidePause = true;
        GetTree().Paused = true;
        Show();
        TextLabel.Text = text;

    }
}
