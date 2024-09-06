using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using scoreboard2.Models;
using scoreboard2.Models.Baseball;

namespace scoreboard2.Controls;

public class InningControl : TemplatedControl
{
    public static readonly StyledProperty<Inning> InningProperty = AvaloniaProperty.Register<InningControl, Inning>(
        "Inning");

    public Inning Inning
    {
        get => GetValue(InningProperty);
        set => SetValue(InningProperty, value);
    }

    private TextBox? _editValueBox;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _editValueBox = e.NameScope.Find<TextBox>(name: "EditValueBox")!;
    }

    public void ButtonClickFunction(int value)
    {
        _editValueBox!.Text = (int.Parse(_editValueBox.Text!) + value).ToString();
    }
}