using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace scoreboard2.Controls;

public class PeriodControl : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<PeriodControl, string>(
        "Title", "PERIOD");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<int> OldPeriodProperty = AvaloniaProperty.Register<PeriodControl, int>(
        "OldPeriod");

    public int OldPeriod
    {
        get => GetValue(OldPeriodProperty);
        set => SetValue(OldPeriodProperty, value);
    }

    public static readonly StyledProperty<int> NewPeriodProperty = AvaloniaProperty.Register<PeriodControl, int>(
        "NewPeriod");

    public int NewPeriod
    {
        get => GetValue(NewPeriodProperty);
        set => SetValue(NewPeriodProperty, value);
    }
    
    public static readonly StyledProperty<ICommand> ButtonClickFunctionProperty = AvaloniaProperty.Register<ScoreButtonsControl, ICommand>(
        "ButtonClickFunction");

    public ICommand ButtonClickFunction
    {
        get => GetValue(ButtonClickFunctionProperty);
        set => SetValue(ButtonClickFunctionProperty, value);
    }

    public static readonly StyledProperty<ICommand> CommitClickFunctionProperty = AvaloniaProperty.Register<PeriodControl, ICommand>(
        "CommitClickFunction");

    public ICommand CommitClickFunction
    {
        get => GetValue(CommitClickFunctionProperty);
        set => SetValue(CommitClickFunctionProperty, value);
    }

    public static readonly StyledProperty<ICommand> RevertClickFunctionProperty = AvaloniaProperty.Register<PeriodControl, ICommand>(
        "RevertClickFunction");

    public ICommand RevertClickFunction
    {
        get => GetValue(RevertClickFunctionProperty);
        set => SetValue(RevertClickFunctionProperty, value);
    }
}