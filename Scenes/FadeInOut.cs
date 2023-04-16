using Godot;
using System;

public partial class FadeInOut : ColorRect
{
    [Export]
    AudioStream Main;
    [Export]
    AudioStream GameOver;
    [Export]
    AudioStream Running;
    [Export]
    AudioStream Drinking;
    MusicStateEnum currentMusic;
    AudioStreamPlayer music = new AudioStreamPlayer();
    public override void _Ready()
    {
        Global.FadeInOut = this;
        AddChild(music);
        music.Stream = Main;
        music.Play();
        music.Bus = "music";
    }
    const float fadeTime = 0.5f;

    public void StartFade(string scene, MusicStateEnum musicStateEnum)
    {
        GetTree().Paused = true;
        Global.FadePause = true;
        var tween = CreateTween();
        tween.TweenProperty(this, "color:a", 1, fadeTime);
        if (currentMusic != musicStateEnum)
        {
            tween.TweenProperty(music, "volume_db", -80, fadeTime);
        }
        tween.Finished += () => FadeIn(musicStateEnum);
        tween.Finished += () => SetScene(scene);
    }

    void SetScene(string scene)
    {
        GetTree().ChangeSceneToFile(scene);
    }
    void FadeIn(MusicStateEnum musicStateEnum)
    {
        var tween = CreateTween();
        if (currentMusic != musicStateEnum)
        {
            switch (musicStateEnum)
            {
                case MusicStateEnum.Main:
                    music.Stream = Main;
                    break;
                case MusicStateEnum.GameOver:
                    music.Stream = GameOver;
                    break;
                case MusicStateEnum.Drinking:
                    music.Stream = Drinking;
                    break;
                case MusicStateEnum.Running:
                    music.Stream = Running;
                    break;
            }
            music.Play();
            tween.TweenProperty(music, "volume_db", 0, fadeTime);
            currentMusic = musicStateEnum;
        }

        tween.TweenProperty(this, "color:a", 0, fadeTime);
        tween.Finished += Unpause;
    }

    void Unpause()
    {
        Global.FadePause = false;
        Global.Unpause();
    }
}
