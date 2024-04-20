using System;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models;

public partial class GameSettings : ObservableObject
{
    [ObservableProperty] private Bitmap? _homeImage;
    [ObservableProperty] private Color _homeBackgroundColor = Colors.White;
    [ObservableProperty] private Color _homeForegroundColor = Colors.Black;

    [ObservableProperty] private Bitmap? _awayImage;
    [ObservableProperty] private Color _awayBackgroundColor = Colors.White;
    [ObservableProperty] private Color _awayForegroundColor = Colors.Black;
}