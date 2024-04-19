using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml.Converters;

namespace scoreboard2.Converters;

public static class AvaloniaStaticConverters
{
    public static readonly IValueConverter ColorToBrushConverter = new ColorToBrushConverter();
}