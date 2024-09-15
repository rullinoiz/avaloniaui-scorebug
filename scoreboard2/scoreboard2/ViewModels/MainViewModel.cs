using System;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models;
using scoreboard2.Models.Baseball;
using scoreboard2.Models.Football;
using scoreboard2.ViewModels.Common;

#if !BROWSER
using Avalonia.Controls;
using scoreboard2.Windows;
#endif

namespace scoreboard2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public BaseGame BaseGame { get; } = new();
    public BaseballModel Baseball { get; } = new();
    public FootballModel Football { get; } = new();
    
    [ObservableProperty] private int _selectedGame;
    
#if !BROWSER
    private record ScorebugSize(double Width, double Height);
    
    private readonly Window? _scoreboardWindow;
    private readonly ScorebugSize[] _scorebugSizes = [
        new ScorebugSize(500, 125),
        new ScorebugSize(1000, 75)
    ];
    
    partial void OnSelectedGameChanged(int value)
    {
        var size = _scorebugSizes[value];
        _scoreboardWindow!.Width = size.Width;
        _scoreboardWindow!.Height = size.Height;
    }
#endif
    
    public MainViewModel()
    {
        Console.WriteLine("instantiating main view model");
        
#if !BROWSER
        _scoreboardWindow = new ScoreboardWindow(this);
        var scorebugConfig = new ScorebugConfig(this);
        _scoreboardWindow.Show();
        scorebugConfig.Show();
        
        OnSelectedGameChanged(SelectedGame);
#endif
    }
}