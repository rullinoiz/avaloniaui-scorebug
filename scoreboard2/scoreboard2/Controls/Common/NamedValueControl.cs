using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace scoreboard2.Controls.Common;

public class NamedValueControl : TemplatedControl
{
    protected TextBox? EditValue;
    
    public virtual void ButtonClickFunction(int value)
    {
        EditValue!.Text = (int.Parse(EditValue.Text!) + value).ToString();
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        EditValue = e.NameScope.Find<TextBox>(name:"EditValueBox")!;
    }
}