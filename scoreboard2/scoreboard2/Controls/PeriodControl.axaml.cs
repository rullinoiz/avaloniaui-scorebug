using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using scoreboard2.Models;

namespace scoreboard2.Controls;

public class PeriodControl : TemplatedControl
{
    public static readonly StyledProperty<Period> PeriodProperty = AvaloniaProperty.Register<PeriodControl, Period>(
        "Period");

    public Period Period
    {
        get => GetValue(PeriodProperty);
        set => SetValue(PeriodProperty, value);
    }
    
    private TextBox? _editValue;
    
    public void ButtonClickFunction(int value)
    {
        _editValue!.Text = (int.Parse(_editValue.Text!) + value).ToString();
    }

    public void CommitClickFunction()
    {
        Period.Value = int.Parse(_editValue!.Text ?? string.Empty);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _editValue = e.NameScope.Find<TextBox>(name:"EditValueBox")!;
        Console.WriteLine($"Loaded {Period.Name}!");
    }

    public PeriodControl()
    {
        Period = new Period();
    }
}