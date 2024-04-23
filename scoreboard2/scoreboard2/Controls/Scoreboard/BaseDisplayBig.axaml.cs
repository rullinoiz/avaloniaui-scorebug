using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using scoreboard2.Common;
using scoreboard2.Models.Baseball;

namespace scoreboard2.Controls.Scoreboard;

public class BaseDisplayBig : TemplatedControl
{
    public static readonly StyledProperty<Base?> BaseProperty = AvaloniaProperty.Register<BaseDisplayBig, Base?>(
        "Base");

    public Base? Base
    {
        get => GetValue(BaseProperty);
        set => SetValue(BaseProperty, value);
    }

    private static readonly IBrush? OnColor = Helper.GetResource<IBrush>("OnColorBrush");
    private static readonly IBrush? OffColor = Helper.GetResource<IBrush>("OffColorBrush");

    private Rectangle? _base1;
    private Rectangle? _base2;
    private Rectangle? _base3;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        Base ??= new Base();

        _base1 = e.NameScope.Find<Rectangle>(name: "Base1");
        _base2 = e.NameScope.Find<Rectangle>(name: "Base2");
        _base3 = e.NameScope.Find<Rectangle>(name: "Base3");

        Base.PropertyChanged += (_, _) =>
        {
            _base1!.Fill = Base.BaseStates[0] ? OnColor : OffColor;
            _base2!.Fill = Base.BaseStates[1] ? OnColor : OffColor;
            _base3!.Fill = Base.BaseStates[2] ? OnColor : OffColor;
        };
    }
}