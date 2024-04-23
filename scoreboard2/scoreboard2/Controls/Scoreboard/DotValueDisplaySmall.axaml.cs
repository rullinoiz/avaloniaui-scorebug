using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using scoreboard2.Common;

namespace scoreboard2.Controls.Scoreboard;

public class DotValueDisplaySmall : TemplatedControl
{
    public static readonly StyledProperty<int> MaxProperty = AvaloniaProperty.Register<DotValueDisplaySmall, int>(
        "Max");

    public int Max
    {
        get => GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public static readonly StyledProperty<int> ValueProperty = AvaloniaProperty.Register<DotValueDisplaySmall, int>(
        "Value");

    public int Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private static readonly IBrush? OnColor = Helper.GetResource<IBrush>("OnColorBrush");
    private static readonly IBrush? OffColor = Helper.GetResource<IBrush>("OffColorBrush");

    private StackPanel? _panel;
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _panel = e.NameScope.Find<StackPanel>(name: "StackPanel")!;

        for (var i = 0; i < Max; i++)
        {
            _panel.Children.Add(new Ellipse
            {
                Fill = OffColor, 
                Width = 15, 
                Height = 15
                // Stroke = Resources["OnColorBrush"] as SolidColorBrush, 
                // StrokeThickness = 2
            });
        }

        PropertyChanged += (_, args) =>
        {
            if (args.Property != ValueProperty) return;
            var x = Value;
            foreach (var i in _panel.Children)
            {
                if (i is not Ellipse r) continue;
                r.Fill = x > 0 ? OnColor : OffColor;
                x--;
            }
        };
    }

}