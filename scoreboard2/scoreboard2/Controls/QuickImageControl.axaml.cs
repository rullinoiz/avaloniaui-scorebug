using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;

namespace scoreboard2.Controls;

public class QuickImageControl : TemplatedControl
{
    public static readonly StyledProperty<Bitmap> ImageProperty = AvaloniaProperty.Register<QuickImageControl, Bitmap>(
        "Image");

    public Bitmap Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<QuickImageControl, string>(
        "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public async void ChooseClickFunction()
    {
        var toplevel = TopLevel.GetTopLevel(this) ?? throw new NullReferenceException(
            "errrm something went wrong");

        var files = await toplevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }
        });

        if (files.Count < 1) return;
        
        await using var stream = await files[0].OpenReadAsync();
        var result = new Bitmap(stream);
        Image = result;
    }
}