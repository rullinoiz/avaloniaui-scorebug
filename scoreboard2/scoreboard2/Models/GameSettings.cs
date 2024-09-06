using System;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models;

public partial class GameSettings : ObservableObject
{
    [ObservableProperty] private Bitmap? _homeImage;
    [ObservableProperty] private Color _homeBackgroundColor = Colors.Black;
    [ObservableProperty] private Color _homeForegroundColor = Colors.White;
    [ObservableProperty] private string _homeCityName = string.Empty;
    [ObservableProperty] private string _homeTeamName = string.Empty;

    [ObservableProperty] private Bitmap? _awayImage;
    [ObservableProperty] private Color _awayBackgroundColor = Colors.Black;
    [ObservableProperty] private Color _awayForegroundColor = Colors.White;
    [ObservableProperty] private string _awayCityName = string.Empty;
    [ObservableProperty] private string _awayTeamName = string.Empty;
}