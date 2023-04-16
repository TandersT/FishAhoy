using Godot;
using System;
using System.Threading.Tasks;

public enum VisualNovelTypeEnum
{
    Start,
    GameOver,
    End,
}

public partial class VisuelNovel : Control
{
    Button ReturnButton;
    NovelPoints NovelPointsLeft, NovelPointsRight;
    TextBox Dialouge;
    [Export]
    Texture2D Start, End;
    TextureRect BG;
    PanelContainer EndContainer;
    Label EndText;

    public override void _Ready()
    {
        NovelPointsLeft = GetNode<NovelPoints>(nameof(NovelPointsLeft));
        NovelPointsRight = GetNode<NovelPoints>(nameof(NovelPointsRight));
        Dialouge = GetNode<TextBox>(nameof(Dialouge));
        BG = GetNode<TextureRect>($"%{nameof(BG)}");
        EndContainer = GetNode<PanelContainer>($"%{nameof(EndContainer)}");
        EndText = GetNode<Label>($"%{nameof(EndText)}");
        ReturnButton = GetNode<Button>($"%{nameof(ReturnButton)}");
        ReturnButton.Pressed += ReturnToMain;



        switch (Global.VisualNovelType)
        {
            case VisualNovelTypeEnum.Start:
                // EndStory();
                StartStory();
                break;
            case VisualNovelTypeEnum.End:
                EndStory();
                break;
        }
    }

    private void ReturnToMain()
    {
        Global.CustomChangeScene(Global.Main, MusicStateEnum.Main);
    }

    public Tween EnterCharacter(NovelPoints novelSide)
    {
        novelSide.Character.GlobalPosition = novelSide.OutsidePoint.GlobalPosition;
        novelSide.Character.Rotation = novelSide.OutsidePoint.Rotation;
        var _tween = CreateTween().SetParallel(true);
        _tween.TweenProperty(novelSide.Character, "global_position", novelSide.InsidePoint.GlobalPosition, 2).SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.InOut);
        _tween.TweenProperty(novelSide.Character, "rotation", 0, 2).SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.InOut);
        return _tween;
    }
    public Tween LeaveCharacter(NovelPoints novelSide)
    {
        var _tween = CreateTween().SetParallel(true);
        _tween.TweenProperty(novelSide.Character, "global_position", novelSide.OutsidePoint.GlobalPosition, 2).SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.InOut);
        _tween.TweenProperty(novelSide.Character, "rotation", -novelSide.OutsidePoint.Rotation, 2).SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.InOut);
        return _tween;
    }
    public void TransformCharacter()
    {

    }
    const int delay = 1500;
    async void StartStory()
    {
        BG.Texture = Start;
        // NovelPointsLeft.Icon = //Man
        // NovelPointsRight.Icon = //Witch
        await ToSignal(EnterCharacter(NovelPointsLeft), "finished");
        await ToSignal(Dialouge.EnterTextBox(), "finished");
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "Mhmm A lovely fish stick. They sure are yummy."), "finished");
        await ToSignal(EnterCharacter(NovelPointsRight), "finished");
        await ToSignal(Dialouge.NewText(NovelPointsRight, "My brethen! How could you! For your sins against my kind, I will make you pay!"), "finished");
        await Task.Delay(delay);
        NovelPointsRight.Icon.Frame = 1;
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(null, "Pooof"), "finished");
        NovelPointsRight.Icon.Frame = 0;
        NovelPointsLeft.Icon.Frame = 1;
        // NovelPointsLeft.Icon = //Fish
        await Task.Delay(delay * 2);
        NovelPointsLeft.Icon.Frame = 2;
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "What the heck? What is this, why am I a fish?"), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsRight, "Yeah that serves you right. This is how it is to be a fish."), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "Man what the heck all I did was eat a fish stick?"), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsRight, "Silence! The only way I will condone you of your sins, is if you manage to complete the 7 seas!"), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "wat?"), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsRight, "The ultimate pubcrawl, that ends at Lands Ahoy! But you have to hurry, I got a flight to Canada tomorrow."), "finished");
        await Task.Delay(delay);

        Global.CustomChangeScene(Global.RunningScene, MusicStateEnum.Running);
    }


    async void EndStory()
    {
        BG.Texture = End;
        NovelPointsLeft.Icon.Frame = 2;
        NovelPointsRight.Icon.Frame = 0;
        await ToSignal(EnterCharacter(NovelPointsLeft), "finished");
        await ToSignal(Dialouge.EnterTextBox(), "finished");
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "Fish wiieee.. witch! Where are youu? *Hick*"), "finished");
        await ToSignal(EnterCharacter(NovelPointsRight), "finished");
        await ToSignal(Dialouge.NewText(NovelPointsRight, "Hmm.. Impressive.. You are still standing."), "finished");
        NovelPointsRight.Icon.Frame = 2;
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "I have made it, drunk beyond any fish and seen all the seven seas.."), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsRight, "Well, a promise is a promise."), "finished");
        await Task.Delay(delay);
        NovelPointsRight.Icon.Frame = 3;
        await Task.Delay(delay);
        NovelPointsLeft.Icon.Frame = 1;
        await ToSignal(Dialouge.NewText(null, "Pooof"), "finished");
        await Task.Delay(delay * 2);
        NovelPointsRight.Icon.Frame = 2;
        NovelPointsLeft.Icon.Frame = 3;
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "Oh boy, finally a human mouth. I got a some delicious salmon sushi waiting for me at home!"), "finished");
        await Task.Delay(delay);
        NovelPointsLeft.Icon.Frame = 4;
        await ToSignal(Dialouge.NewText(NovelPointsRight, "You got WHAAT AT HOME!!?"), "finished");
        await Task.Delay(delay);
        await ToSignal(Dialouge.NewText(NovelPointsLeft, "aw shiieee..."), "finished");
        await Task.Delay(delay);
        await ToSignal(LeaveCharacter(NovelPointsLeft), "finished");
        var text = EndText.Text;
        text = text.Replace("@TIME@", Global.stopwatch.Elapsed.Minutes.ToString("00") + ":" + Global.stopwatch.Elapsed.Seconds.ToString("00"));
        if (Global.stopwatch.Elapsed.TotalSeconds > Global.Highscore)
        {
            Global.Highscore = Global.stopwatch.Elapsed.TotalSeconds;
            Global.HighscoreString = Global.stopwatch.Elapsed.Minutes.ToString("00") + ":" + Global.stopwatch.Elapsed.Seconds.ToString("00");
        }
        text = text.Replace("@RECORD@", Global.HighscoreString);
        EndText.Text = text;
        EndContainer.Visible = true;
    }
}
