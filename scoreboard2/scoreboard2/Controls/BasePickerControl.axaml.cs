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

        Base.PropertyChanged += (_, _) =>
        {
            // if (args.PropertyName != nameof(Base.BaseStates)) return;
            _base1!.Background = Base.BaseStates[0] ? OnColor : OffColor;
            _base2!.Background = Base.BaseStates[1] ? OnColor : OffColor;
            _base3!.Background = Base.BaseStates[2] ? OnColor : OffColor;
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