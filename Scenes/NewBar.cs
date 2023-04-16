using Godot;
using System;
using System.Threading.Tasks;

public partial class NewBar : Control
{
    
    Label BarName;
    public async override void _Ready()
    {
        BarName = GetNode<Label>($"%{nameof(BarName)}");
		BarName.Text = Global.BarNames[Global.CurrentLevel];
		await Task.Delay(4000);
		Global.CustomChangeScene(Global.DrinkingScene, MusicStateEnum.Drinking);
    }
}
