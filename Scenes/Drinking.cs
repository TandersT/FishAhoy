using Godot;
using System;

public partial class Drinking : Control
{
    int Score;
    Label ScoreValue;
    Area2D MouthArea;

    public void SetLevelDifficulty(RunningData data)
    {

    }
    public override void _Ready()
    {
        ScoreValue = GetNode<Label>($"%{nameof(ScoreValue)}");
        MouthArea = GetNode<Area2D>($"%{nameof(MouthArea)}");

        MouthArea.BodyEntered += UpdateScore;
    }

    private void UpdateScore(Node2D area)
    {
        if (area is BeerParticle beer)
        {
            if (!beer.isUsed)
            {
                Score++;
                ScoreValue.Text = Score.ToString();
                beer.isUsed = true;
            }
        }
    }
}
