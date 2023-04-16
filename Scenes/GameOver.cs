using Godot;
using System;

public partial class GameOver : Control
{
    Label BarValue;
    Button TryAgain, Exit;
    public override void _Ready()
    {
        BarValue = GetNode<Label>($"%{nameof(BarValue)}");
        TryAgain = GetNode<Button>($"%{nameof(TryAgain)}");
        Exit = GetNode<Button>($"%{nameof(Exit)}");
		TryAgain.Pressed += OnTryAgain;
		Exit.Pressed += OnReset;

		BarValue.Text = (Global.CurrentLevel + 1).ToString();
    }

    private void OnReset()
    {
		Global.CustomChangeScene(Global.Main, MusicStateEnum.Main);
    }

    private void OnTryAgain()
    {
		Global.CustomChangeScene(Global.LatestState, Global.LatestMusicState);
    }
}
