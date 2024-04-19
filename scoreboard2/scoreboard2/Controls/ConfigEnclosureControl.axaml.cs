using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace scoreboard2.Controls;

public class ConfigEnclosureControl : ContentControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<ConfigEnclosureControl, string>(
        "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<object?> ValueProperty = AvaloniaProperty.Register<ConfigEnclosureControl, object?>(
        "Value");

    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property != ValueProperty) return;
        
        if (change.OldValue is ILogical oldChild)
        {
            LogicalChildren.Remove(oldChild);
        }

        if (change.NewValue is ILogical newChild)
        {
            LogicalChildren.Add(newChild);
        }
    }
}