using Avalonia;
using Avalonia.Controls.Primitives;
using scoreboard2.Converters;
using scoreboard2.Models.Common;

namespace scoreboard2.Controls.Football;

public class DownsAndYards2Go : TemplatedControl
{
    public static readonly StyledProperty<TemporaryNamedValue> DownProperty = AvaloniaProperty.Register<DownsAndYards2Go, TemporaryNamedValue>(
        "Down");

    public TemporaryNamedValue Down
    {
        get => GetValue(DownProperty);
        set => SetValue(DownProperty, value);
    }

    public static readonly StyledProperty<TemporaryNamedValue> YardsProperty = AvaloniaProperty.Register<DownsAndYards2Go, TemporaryNamedValue>(
        "Yards");

    public TemporaryNamedValue Yards
    {
        get => GetValue(YardsProperty);
        set => SetValue(YardsProperty, value);
    }

    public static readonly StyledProperty<string> OutputProperty = AvaloniaProperty.Register<DownsAndYards2Go, string>(
        "Output");

    public string Output
    {
        get => GetValue(OutputProperty);
        private set => SetValue(OutputProperty, value);
    }

    public static readonly StyledProperty<bool> HiddenProperty = AvaloniaProperty.Register<DownsAndYards2Go, bool>(
        "Hidden");

    public bool Hidden
    {
        get => GetValue(HiddenProperty);
        set => SetValue(HiddenProperty, value);
    }

    public void DownModify(int value) => Down.Value += value;

    public void Reset()
    {
        Yards.Clear();
        Down.Clear();
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        PropertyChanged += (_, args) =>
        {
            if (args.Property != DownProperty && args.Property != YardsProperty) return;

            Output = $"{StringTercer.Convert(Down.Value)} & {Yards.Value}";
        };
    }
}