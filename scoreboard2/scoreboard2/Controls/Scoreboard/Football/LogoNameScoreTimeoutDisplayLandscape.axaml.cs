using System;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace scoreboard2.Controls.Scoreboard.Football;

public class LogoNameScoreTimeoutDisplayLandscape : TemplatedControl
{
    public static readonly StyledProperty<Bitmap> ImageProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, Bitmap>(
        "Image");

    public Bitmap? Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty!, value);
    }

    public static readonly StyledProperty<string> CityNameProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, string>(
        "CityName");

    public string CityName
    {
        get => GetValue(CityNameProperty);
        set => SetValue(CityNameProperty, value);
    }

    public static readonly StyledProperty<string> TeamNameProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, string>(
        "TeamName");

    public string TeamName
    {
        get => GetValue(TeamNameProperty);
        set => SetValue(TeamNameProperty, value);
    }

    public static readonly StyledProperty<int> ScoreProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, int>(
        "Score");

    public int Score
    {
        get => GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }

    public static readonly StyledProperty<Color?> FontColorProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, Color?>(
        "FontColor");

    public Color? FontColor
    {
        get => GetValue(FontColorProperty);
        set => SetValue(FontColorProperty, value);
    }

    public static readonly StyledProperty<int> TimeoutsProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, int>(
        "Timeouts");

    public int Timeouts
    {
        get => GetValue(TimeoutsProperty);
        set => SetValue(TimeoutsProperty, value);
    }

    public static readonly StyledProperty<bool> HasBallProperty = AvaloniaProperty.Register<LogoNameScoreTimeoutDisplayLandscape, bool>(
        "HasBall");

    public bool HasBall
    {
        get => GetValue(HasBallProperty);
        set => SetValue(HasBallProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        try
        {
            Image ??= new Bitmap(AssetLoader.Open(new Uri("avares://scoreboard2/Common/Assets/clist_dir.png")));
        }
        catch (System.IO.DirectoryNotFoundException exception)
        {
            Console.WriteLine(exception);
        }
        
        FontColor ??= Colors.Black;
        Score = 0;
    }
}