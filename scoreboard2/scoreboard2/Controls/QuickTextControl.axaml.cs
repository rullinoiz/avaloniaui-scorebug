using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace scoreboard2.Controls;

public class QuickTextControl : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<QuickTextControl, string>(
        "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<QuickTextControl, string>(
        "Value");

    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}