using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using scoreboard2.Common;
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

    private static IBrush? _onColor;
    private static IBrush? _offColor;

    private Button? _base1;
    private Button? _base2;
    private Button? _base3;

    public BasePickerControl()
    {
        var x = Application.Current;
        
        _onColor ??= this.GetResource<IBrush>("OnColor");
        _offColor ??= this.GetResource<IBrush>("OffColor");
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        Console.WriteLine(_onColor is null);
        

        Base ??= new Base();

        _base1 = e.NameScope.Find<Button>(name: "Base1");
        _base2 = e.NameScope.Find<Button>(name: "Base2");
        _base3 = e.NameScope.Find<Button>(name: "Base3");

        Base.PropertyChanged += (_, _) =>
        {
            // if (args.PropertyName != nameof(Base.BaseStates)) return;
            _base1!.Background = Base.BaseStates[0] ? _onColor : _offColor;
            _base2!.Background = Base.BaseStates[1] ? _onColor : _offColor;
            _base3!.Background = Base.BaseStates[2] ? _onColor : _offColor;
        };
    }
}