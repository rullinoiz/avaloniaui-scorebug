using Avalonia;
using Avalonia.Controls.Primitives;
using scoreboard2.Models;

namespace scoreboard2.Controls;

public class ClockControl : TemplatedControl
{
    public static readonly StyledProperty<Clock> ClockProperty = AvaloniaProperty.Register<ClockControl, Clock>(
        nameof(Clock));

    public Clock Clock
    {
        get => GetValue(ClockProperty);
        set => SetValue(ClockProperty, value);
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<ClockControl, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}