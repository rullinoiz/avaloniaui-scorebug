using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace scoreboard2.Controls;

public class ScoreButtonsControl : TemplatedControl
{
    public static readonly StyledProperty<int> ScoreProperty = AvaloniaProperty.Register<ScoreButtonsControl, int>(
        nameof(Score), defaultValue:420);

    public int Score
    {
        get => GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }

    public static readonly StyledProperty<double> ScoreBoundsWidthProperty = AvaloniaProperty.Register<ScoreButtonsControl, double>(
        nameof(ScoreBoundsWidth), defaultValue:45);

    public double ScoreBoundsWidth
    {
        get => GetValue(ScoreBoundsWidthProperty);
        set => SetValue(ScoreBoundsWidthProperty, value);
    }

    public static readonly StyledProperty<ICommand> ButtonClickFunctionProperty = AvaloniaProperty.Register<ScoreButtonsControl, ICommand>(
        "ButtonClickFunction");

    public ICommand ButtonClickFunction
    {
        get => GetValue(ButtonClickFunctionProperty);
        set => SetValue(ButtonClickFunctionProperty, value);
    }
}