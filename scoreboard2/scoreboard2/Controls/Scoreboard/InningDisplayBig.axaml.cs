using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using scoreboard2.Models.Baseball;

namespace scoreboard2.Controls.Scoreboard;

public class InningDisplayBig : TemplatedControl
{
    public static readonly StyledProperty<Inning?> InningProperty = AvaloniaProperty.Register<InningDisplayBig, Inning?>(
        "Inning");

    public Inning? Inning
    {
        get => GetValue(InningProperty);
        set => SetValue(InningProperty, value);
    }
    

    private static readonly Uri OnArrowPath = new ("avares://scoreboard2/Controls/Common/Assets/inning_arrow_on.png");
    private static readonly Uri OffArrowPath = new ("avares://scoreboard2/Controls/Common/Assets/inning_arrow_off.png");

    private static readonly Bitmap OnArrow = new (AssetLoader.Open(OnArrowPath));
    private static readonly Bitmap OffArrow = new (AssetLoader.Open(OffArrowPath));
    
    private Image? _topArrow;
    private Image? _bottomArrow;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        Inning ??= new Inning();
        
        _topArrow = e.NameScope.Find<Image>(name: "TopArrow")!;
        _bottomArrow = e.NameScope.Find<Image>(name: "BottomArrow")!;
        
        Inning.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName != nameof(Inning.TopOrBottom)) return;
            
            var topOrBottom = Inning.TopOrBottom;
            _topArrow!.Source = topOrBottom == InningType.Top ? OnArrow : OffArrow;
            _bottomArrow!.Source = topOrBottom == InningType.Bottom ? OnArrow : OffArrow;
        };
        
        var topOrBottom = Inning!.TopOrBottom;
        _topArrow!.Source = topOrBottom == InningType.Top ? OnArrow : OffArrow;
        _bottomArrow!.Source = topOrBottom == InningType.Bottom ? OnArrow : OffArrow;
    }
}