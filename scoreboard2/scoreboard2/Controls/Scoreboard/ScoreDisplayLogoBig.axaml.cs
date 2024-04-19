using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace scoreboard2.Controls.Scoreboard;

public class ScoreDisplayLogoBig : TemplatedControl
{
    public static readonly StyledProperty<int> ValueProperty = AvaloniaProperty.Register<ScoreDisplayLogoBig, int>(
        "Value");

    public int Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly StyledProperty<Bitmap> ImageProperty = AvaloniaProperty.Register<ScoreDisplayLogoBig, Bitmap>(
        "Image");

    public Bitmap Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly StyledProperty<Color> FontColorProperty = AvaloniaProperty.Register<ScoreDisplayLogoBig, Color>(
        "FontColor");

    public Color FontColor
    {
        get => GetValue(FontColorProperty);
        set => SetValue(FontColorProperty, value);
    }

    public ScoreDisplayLogoBig()
    {
        try
        {
            Image = new Bitmap("../../../../scoreboard2/Common/Assets/clist_dir.png");
        } 
        catch (System.IO.DirectoryNotFoundException) { }

        FontColor = Colors.Black;
    }
}