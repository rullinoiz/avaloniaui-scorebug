using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using scoreboard2.Models.Baseball;

namespace scoreboard2.Controls.Scoreboard.Baseball;

public class BaseDisplayBig : TemplatedControl
{
    public static readonly StyledProperty<Base?> BaseProperty = AvaloniaProperty.Register<BaseDisplayBig, Base?>(
        "Base");

    public Base? Base
    {
        get => GetValue(BaseProperty);
        set => SetValue(BaseProperty, value);
    }

    private static readonly IBrush OnColor = new SolidColorBrush(0xffffa709);
    private static readonly IBrush OffColor = new SolidColorBrush(0xff442d0a);

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

        Base.Base1.PropertyChanged += (_, _) =>
        {
            _base1!.Fill = Base.Base1.State ? OnColor : OffColor;
        };
        Base.Base2.PropertyChanged += (_, _) =>
        {
            _base2!.Fill = Base.Base2.State ? OnColor : OffColor;
        };
        Base.Base3.PropertyChanged += (_, _) =>
        {
            _base3!.Fill = Base.Base3.State ? OnColor : OffColor;
        };
    }
}