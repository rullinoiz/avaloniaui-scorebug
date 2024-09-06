using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

using static scoreboard2.Common.Extensions.ArrayTryGetValue;

namespace scoreboard2.Converters;

public class StringTercer : IValueConverter
{
    public static readonly StringTercer Instance = new();

    private static readonly string[] Suffixes =
    [
        "st",
        "nd",
        "rd",
        "th"
    ];

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value ?? string.Empty;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is int i ? Convert(i) : null;
    
    public static string Convert(int value) 
        => $"{value}{(Suffixes.TryGetValue(value - 1, out var t) ? t : Suffixes.Last())}";
}