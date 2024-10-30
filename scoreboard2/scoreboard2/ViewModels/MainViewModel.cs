using System;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models;
using scoreboard2.Models.Baseball;
using scoreboard2.Models.Football;
using scoreboard2.RemoteControl.Attributes;
using scoreboard2.ViewModels.Common;
using scoreboard2.Common;

#if !(BROWSER || IOS || ANDROID)
using Avalonia.Controls;
using scoreboard2.Windows;
#endif

#pragma warning disable CS0657

namespace scoreboard2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public BaseGame BaseGame { get; } = new();
    public BaseballModel Baseball { get; } = new();
    public FootballModel Football { get; } = new();
    
    [ObservableProperty] private int _selectedGame;
    
    [ObservableProperty] 
    [property: ReplicatorIgnore]
    private double _scoreboardWidth;
    
    [ObservableProperty] 
    [property: ReplicatorIgnore]
    private double _scoreboardHeight;
    
#if !(BROWSER || IOS || ANDROID)
    private readonly Window? _scoreboardWindow;
#endif
    
    private readonly ScorebugSize[] _scorebugSizes = [
        new (500, 125),
        new (1000, 75)
    ];
    
    partial void OnSelectedGameChanged(int value)
    {
        var size = _scorebugSizes[value];
        ScoreboardWidth = size.Width;
        ScoreboardHeight = size.Height;
#if !(BROWSER || IOS || ANDROID)
        _scoreboardWindow!.Width = size.Width;
        _scoreboardWindow!.Height = size.Height;
#endif
    }
    
    public MainViewModel()
    {
        Console.WriteLine("instantiating main view model");
        
#if !(BROWSER || IOS || ANDROID)
        _scoreboardWindow = new ScoreboardWindow(this);
        var scorebugConfig = new ScorebugConfig(this);
        _scoreboardWindow.Show();
        scorebugConfig.Show();
#endif
        
        OnSelectedGameChanged(SelectedGame);
    }
}