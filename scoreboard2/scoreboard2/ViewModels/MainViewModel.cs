using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models;
using scoreboard2.Models.Baseball;
using scoreboard2.Models.Football;
using scoreboard2.ViewModels.Common;
using scoreboard2.Windows;

namespace scoreboard2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static string Ampersand => "&";

    private record ScorebugSize(double Width, double Height);
    
    public BaseGame BaseGame { get; } = new();
    public BaseballModel Baseball { get; } = new();
    public FootballModel Football { get; } = new();
    private readonly Window _scoreboardWindow;
    private readonly ScorebugSize[] _scorebugSizes = [
        new ScorebugSize(500, 125),
        new ScorebugSize(1000, 75)
    ];
    
    [ObservableProperty] private int _selectedGame;
    partial void OnSelectedGameChanged(int value)
    {
        var size = _scorebugSizes[value];
        _scoreboardWindow.Width = size.Width;
        _scoreboardWindow.Height = size.Height;
    }

    public MainViewModel()
    {
        Console.WriteLine("instantiating main view model");
        
        _scoreboardWindow = new ScoreboardWindow(this);
        var scorebugConfig = new ScorebugConfig(this);
        OnSelectedGameChanged(SelectedGame);
        _scoreboardWindow.Show();
        scorebugConfig.Show();
    }
}