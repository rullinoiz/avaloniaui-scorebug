using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using scoreboard2.Controls.Common;
using scoreboard2.Models;

namespace scoreboard2.Controls;

public class ScoreControl : NamedValueControl
{
    public static readonly StyledProperty<Score> ScoreProperty = AvaloniaProperty.Register<ScoreControl, Score>(
        "Score");

    public Score Score
    {
        get => GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }

    private TextBox? _editValue;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _editValue = e.NameScope.Find<TextBox>(name: "EditValueBox")!;
    }
    
    public void ResetClickFunction() => _editValue!.Text = Score.Value.ToString();

}