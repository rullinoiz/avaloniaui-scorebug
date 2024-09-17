using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using scoreboard2.Models.Baseball;

namespace scoreboard2.Controls;

public class BasePickerControl : TemplatedControl
{
    public static readonly StyledProperty<Base?> BaseProperty = AvaloniaProperty.Register<BasePickerControl, Base?>(
        "Base");

    public Base? Base
    {
        get => GetValue(BaseProperty);
        set => SetValue(BaseProperty, value);
    }
    
    private static readonly IBrush OnColor = new SolidColorBrush(0xffffa709);
    private static readonly IBrush OffColor = new SolidColorBrush(0xff442d0a);

    private Button? _base1;
    private Button? _base2;
    private Button? _base3;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
    
        Base ??= new Base();
    
        _base1 = e.NameScope.Find<Button>(name: "Base1");
        _base2 = e.NameScope.Find<Button>(name: "Base2");
        _base3 = e.NameScope.Find<Button>(name: "Base3");

        Base.Base1.PropertyChanged += (_, _) =>
        {
            _base1!.Background = Base.Base1.State ? OnColor : OffColor;
        };
        Base.Base2.PropertyChanged += (_, _) =>
        {
            _base2!.Background = Base.Base2.State ? OnColor : OffColor;
        };
        Base.Base3.PropertyChanged += (_, _) =>
        {
            _base3!.Background = Base.Base3.State ? OnColor : OffColor;
        };
    }

    public void ButtonClickFunction(string tag)
    {
        Base!.Toggle(tag switch
        {
            "Base1" => 0,
            "Base2" => 1,
            "Base3" => 2,
            _ => 0
        });
    }
}