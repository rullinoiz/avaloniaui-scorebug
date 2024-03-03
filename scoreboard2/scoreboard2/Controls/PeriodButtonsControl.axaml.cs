using System;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace scoreboard2.Controls;

public class PeriodButtonsControl : TemplatedControl
{
    public static readonly StyledProperty<int> PeriodProperty = AvaloniaProperty.Register<PeriodButtonsControl, int>(
        "Period", defaultValue:2);

    public int Period
    {
        get => GetValue(PeriodProperty);
        set => SetValue(PeriodProperty, value);
    }

    public static readonly StyledProperty<double> PeriodBoundsWidthProperty = AvaloniaProperty.Register<PeriodButtonsControl, double>(
        "PeriodBoundsWidth", defaultValue:45);

    public double PeriodBoundsWidth
    {
        get => GetValue(PeriodBoundsWidthProperty);
        set => SetValue(PeriodBoundsWidthProperty, value);
    }

    public PeriodButtonsControl()
    {
        DataContext = this;
    }

    public static readonly StyledProperty<Action<int>> ButtonClickFunctionProperty = AvaloniaProperty.Register<PeriodButtonsControl, Action<int>>(
        "ButtonClickFunction");

    public Action<int> ButtonClickFunction
    {
        get => GetValue(ButtonClickFunctionProperty);
        set => SetValue(ButtonClickFunctionProperty, value);
    }
}