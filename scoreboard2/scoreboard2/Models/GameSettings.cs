using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.RemoteControl.Attributes;

#pragma warning disable CS0657

namespace scoreboard2.Models;

public partial class GameSettings : ObservableObject
{
    [ObservableProperty] 
    [property: ReplicatorIgnore]
    private Bitmap? _homeImage;
    
    [ObservableProperty] private Color _homeBackgroundColor = Colors.Black;
    [ObservableProperty] private Color _homeForegroundColor = Colors.White;
    [ObservableProperty] private string _homeCityName = string.Empty;
    [ObservableProperty] private string _homeTeamName = string.Empty;
    
    [ObservableProperty]
    [property: ReplicatorIgnore]
    private Bitmap? _awayImage;
    
    [ObservableProperty] private Color _awayBackgroundColor = Colors.Black;
    [ObservableProperty] private Color _awayForegroundColor = Colors.White;
    [ObservableProperty] private string _awayCityName = string.Empty;
    [ObservableProperty] private string _awayTeamName = string.Empty;
    
    [ObservableProperty]
    [property: ReplicatorIgnore]
    private string _replicatorUrl = string.Empty;
}