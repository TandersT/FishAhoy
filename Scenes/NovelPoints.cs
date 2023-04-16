using Godot;
using System;

public partial class NovelPoints : Node2D
{
    public Node2D OutsidePoint, InsidePoint, Character;
    public AudioStreamPlayer2D Voice;
    [Export]
    public AudioStream VoiceStream;
    [Export]
    public Texture2D SpeakerImage;

    public Sprite2D Icon;

    public bool isTalking;
    public override void _Ready()
    {
        Icon = GetNode<Sprite2D>($"%{nameof(Icon)}");
        OutsidePoint = GetNode<Node2D>(nameof(OutsidePoint));
        InsidePoint = GetNode<Node2D>(nameof(InsidePoint));
        Character = GetNode<Node2D>(nameof(Character));
        Voice = Character.GetNode<AudioStreamPlayer2D>(nameof(Voice));
        Voice.Stream = VoiceStream;
        Voice.Finished += AudioTalk;
    }

    public void AudioTalk()
    {
        if (isTalking)
        {
            Voice.Play();
        }
    }
}
