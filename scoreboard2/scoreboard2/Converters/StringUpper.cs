using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace scoreboard2.Converters;

public class StringUpper : IValueConverter
{
    public static readonly StringUpper Instance = new();

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value ?? string.Empty;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string i)
        {
            return i.ToUpper();
        }
        return value!.ToString();
    }
}