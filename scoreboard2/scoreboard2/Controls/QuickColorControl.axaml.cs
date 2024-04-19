using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace scoreboard2.Controls;

public class QuickColorControl : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<QuickColorControl, string>(
        "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<QuickColorControl, Color>(
        "Color");

    public Color Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
}