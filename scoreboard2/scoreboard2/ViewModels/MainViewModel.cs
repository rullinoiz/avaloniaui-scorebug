using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models;
using scoreboard2.Windows;

namespace scoreboard2.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public Game Game { get; } = new();
    
    [ObservableProperty] private Action<int> _homeCommit;
    [ObservableProperty] private Action<int> _awayCommit;
    [ObservableProperty] private Action<int> _periodCommit;

    [ObservableProperty] private int _homeScore;
    [ObservableProperty] private int _awayScore;
    [ObservableProperty] private int _period;

    [ObservableProperty] private int _outs;
    [ObservableProperty] private int _balls;
    [ObservableProperty] private int _strikes;

    [ObservableProperty] private Bitmap _homeImage;
    [ObservableProperty] private Bitmap _awayImage;

    private const string HomeTitle = "HOME";
    public string HomeTitleProperty { get; } = HomeTitle;
    private const string AwayTitle = "AWAY";
    public string AwayTitleProperty { get; } = AwayTitle;
    private const string PeriodTitle = "PERIOD";
    public string PeriodTitleProperty { get; } = PeriodTitle;

    private readonly Window _scoreboardWindow;

    public MainViewModel()
    {
        Console.WriteLine("instantiating");
        
        HomeCommit = (value) =>
        {
            HomeScore = value;
        };

        AwayCommit = (value) =>
        {
            AwayScore = value;
        };

        PeriodCommit = (value) => Period = value;

        _scoreboardWindow = new ScoreboardWindow(this);
        _scoreboardWindow.Show();
    }
}