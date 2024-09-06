using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace scoreboard2.Controls.Scoreboard;

public class BarValueDisplay : TemplatedControl
{
    public static readonly StyledProperty<int> MaxProperty = AvaloniaProperty.Register<BarValueDisplay, int>(
        "Max");

    public int Max
    {
        get => GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public static readonly StyledProperty<int> ValueProperty = AvaloniaProperty.Register<BarValueDisplay, int>(
        "Value");

    public int Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly StyledProperty<double> SpacingProperty = AvaloniaProperty.Register<BarValueDisplay, double>(
        "Spacing");

    public double Spacing
    {
        get => GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    private StackPanel? _panel;

    private double _getWidth() => Bounds.Width;
    private double _getHeight() => Bounds.Height;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _panel = e.NameScope.Find<StackPanel>(name: "Container")!;
        
        for (var i = 0; i < Max; i++)
        {
            _panel.Children.Add(new Rectangle
            {
                Fill = new SolidColorBrush(0xffffffff),
                IsVisible = Value - i > 0
            });
        }
        
        // I use LayoutUpdated because the Bounds are zero before the template is applied
        LayoutUpdated += (_, _) =>
        {
            foreach (var i in _panel.Children)
            {
                if (i is not Rectangle r) continue;
                r.Width = (_getWidth() / Max) - (Spacing * (1 + (1f / Max)) - (2 * Spacing) / Max);
                r.Height = _getHeight();
            }
        };

        PropertyChanged += (_, args) =>
        {
            if (args.Property != ValueProperty) return;
            Console.WriteLine($"{_getWidth()}, {_getHeight()}");
            var x = Value;
            foreach (var i in _panel.Children)
            {
                if (i is not Rectangle r) continue;
                r.IsVisible = x > 0;
                x--;
            }
        };
    }
}