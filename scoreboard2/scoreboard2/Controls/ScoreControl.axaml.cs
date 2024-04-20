using Avalonia;
using scoreboard2.Controls.Common;
using scoreboard2.Models;

namespace scoreboard2.Controls;

public class ScoreControl : NamedValueControl
{
    public static readonly StyledProperty<Score> ScoreProperty = AvaloniaProperty.Register<ScoreControl, Score>(
        "Score");

    public Score Score
    {
        get => GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }
}