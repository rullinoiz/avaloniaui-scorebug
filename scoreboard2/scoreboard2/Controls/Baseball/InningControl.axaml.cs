using Avalonia;
using Avalonia.Controls.Primitives;
using scoreboard2.Models.Baseball;

namespace scoreboard2.Controls.Baseball;

public class InningControl : TemplatedControl
{
    public static readonly StyledProperty<Inning> InningProperty = AvaloniaProperty.Register<InningControl, Inning>(
        "Inning");

    public Inning Inning
    {
        get => GetValue(InningProperty);
        set => SetValue(InningProperty, value);
    }
    
    public void ButtonClickFunction(int value)
    {
        Inning.Value += value;
    }
}