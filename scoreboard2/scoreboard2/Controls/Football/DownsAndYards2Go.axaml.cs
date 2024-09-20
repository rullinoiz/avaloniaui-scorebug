using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using scoreboard2.Models.Football;

namespace scoreboard2.Controls.Football;

public class DownsAndYards2Go : TemplatedControl
{
    public static readonly StyledProperty<DownAndYards?> DownAndYardsProperty = AvaloniaProperty.Register<DownsAndYards2Go, DownAndYards?>(
        "DownAndYards");

    public DownAndYards? DownAndYards
    {
        get => GetValue(DownAndYardsProperty);
        set => SetValue(DownAndYardsProperty, value);
    }
    
#nullable disable
    private TextBox _editTextBox;
#nullable enable

    public void DownModify(int value) => DownAndYards!.Down += value;

    public void Reset()
    {
        DownAndYards!.Yards = 10;
        DownAndYards!.Down = 0;
    }

    private void ConfirmYards(string text)
    {
        try
        {
            DownAndYards!.Yards = int.Parse(text);
        }
        catch (System.FormatException)
        {
            DownAndYards!.Yards = 0;
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        DownAndYards ??= new DownAndYards();

        _editTextBox = e.NameScope.Get<TextBox>(name: "YardsTextBox");
        _editTextBox.KeyDown += (_, args) =>
        {
            if (args.Key != Key.Enter) return;
            ConfirmYards(_editTextBox.Text ?? "0");
            DownAndYards.Long = false;
            DownAndYards.Goal = false;
        };
        _editTextBox.LostFocus += (_, _) => { _editTextBox.Text = DownAndYards!.Yards.ToString(); };
    }
}