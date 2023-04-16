using Godot;
using System;
using System.Threading.Tasks;

public partial class Midway : Control
{
    // Called when the node enters the scene tree for the first time.
    TextBox Dialouge;
    NovelPoints NovelPointsLeft;
    Label BarName;
    public override void _Ready()
    {
        Dialouge = GetNode<TextBox>(nameof(Dialouge));
        NovelPointsLeft = GetNode<NovelPoints>(nameof(NovelPointsLeft));
        BarName = GetNode<Label>(nameof(BarName));
        StartStory();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    int delay = 1500;
    async void StartStory()
    {
        BarName.Text = Global.SkippedBarNames[0];
        await ToSignal(Dialouge.EnterTextBox(), "finished");
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "And on I went. From The Cod's Corner"), "finished");
        await Task.Delay(delay);
        BarName.Text = Global.SkippedBarNames[1];
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "To The Siren's Sip *Hick*"), "finished");
        await Task.Delay(delay);
        BarName.Text = Global.SkippedBarNames[2];
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "Aand The Angler's Alehouse. *Hick* Aaalmosst theres,!"), "finished");
        await Task.Delay(delay);
        BarName.Text = Global.BarNames[Global.CurrentLevel];
        await ToSignal(Dialouge.NewText(NovelPointsLeft, $"Second to last.. {Global.BarNames[Global.CurrentLevel]}. "), "finished");
		await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsLeft, $"My *Hick* arm started swinging real bad..."), "finished");
        await Task.Delay(delay);
        Global.CustomChangeScene(Global.DrinkingScene, MusicStateEnum.Drinking);
    }
}
