using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using scoreboard2.Models.Common;

namespace scoreboard2.Controls;

public class WrapAroundClickerControl : TemplatedControl
{
    public static readonly StyledProperty<NamedValue> ValueProperty = AvaloniaProperty.Register<WrapAroundClickerControl, NamedValue>(
        "Value");

    public NamedValue Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly StyledProperty<int> MaxProperty = AvaloniaProperty.Register<WrapAroundClickerControl, int>(
        "Max");

    public int Max
    {
        get => GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public static readonly StyledProperty<Action?> WrapFunctionProperty = AvaloniaProperty.Register<WrapAroundClickerControl, Action?>(
        "WrapFunction");

    public Action? WrapFunction
    {
        get => GetValue(WrapFunctionProperty);
        set => SetValue(WrapFunctionProperty, value);
    }

    private CheckBox? _enableWrap;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _enableWrap = e.NameScope.Find<CheckBox>(name: "EnableWrapCallback")!;
    }

    public void ButtonClickFunction(int value)
    {
        if (Value.Value + value > Max)
        {
            Value.Value = 0;
            if (_enableWrap!.IsChecked!.Value) WrapFunction!.Invoke();
            return;
        }

        if (Value.Value + value < 0)
        {
            Value.Value = Max;
            return;
        }

        Value.Value += value;
    }
}