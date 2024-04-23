using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace scoreboard2.Common;

public static class Helper
{
    public static readonly IResourceDictionary StaticResources = Application.Current!.Resources;

    private static readonly Dictionary<string, object?> Cache = new();
    
    public static T? GetResource<T>(string key) where T : class
    {
        if (Cache.TryGetValue(key, out var o))
        {
            return o as T;
        }

        var result = !Application.Current!.TryGetResource(key, Application.Current!.ActualThemeVariant, out var v) ? null : v as T;
        Cache.Add(key, result);
        return result;
    }

    public static T? GetResource<T>(this Control c, string key) where T : class
    {
        return c.TryGetResource(key, Application.Current!.ActualThemeVariant, out var v) ? null : v as T;
    }
}