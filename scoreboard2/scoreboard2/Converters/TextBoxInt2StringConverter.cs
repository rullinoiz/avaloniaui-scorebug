using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace scoreboard2.Converters;

#pragma warning disable CS8602
#pragma warning disable CS8604
public class TextBoxInt2StringConverter : IValueConverter
{
    public static readonly TextBoxInt2StringConverter Instance = new();
    
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => int.Parse(value.ToString());

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int i)
        {
            return i.ToString();
        }
        return null;
    }
}