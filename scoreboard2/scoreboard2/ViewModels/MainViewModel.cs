using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.ViewModels;

public partial class MainViewModel : ObservableObject
{
    // public void HomeScoreFunction(int value) => HomeScore += value;
    //
    // public void AwayScoreFunction(int value) => AwayScore += value;
    //
    // public void PeriodFunction(int value) => Period += value;

    [ObservableProperty] private Action<int> _homeCommit;
    [ObservableProperty] private Action<int> _awayCommit;
    [ObservableProperty] private Action<int> _periodCommit;

    // public bool CanRevert(string name)
    // {
    //     return name switch
    //     {
    //         HomeTitle => _prevHomeScore.Count > 0,
    //         AwayTitle => _prevAwayScore.Count > 0,
    //         _ => false
    //     };
    // }
    //
    // public void Revert(string name)
    // {
    //     switch (name)
    //     {
    //         case HomeTitle:
    //             HomeScore = _prevHomeScore.Pop();
    //             break;
    //         case AwayTitle:
    //             AwayScore = _prevAwayScore.Pop();
    //             break;
    //     }
    // }
    
    [ObservableProperty] private int _homeScore;
    [ObservableProperty] private int _currentHomeScore;
    private readonly Stack<int> _prevHomeScore = new();
    

    [ObservableProperty] private int _awayScore;
    [ObservableProperty] private int _currentAwayScore;
    private readonly Stack<int> _prevAwayScore = new();

    [ObservableProperty] private int _period;
    [ObservableProperty] private int _currentPeriod;

    private const string HomeTitle = "HOME";
    public string HomeTitleProperty { get; } = HomeTitle;
    private const string AwayTitle = "AWAY";
    public string AwayTitleProperty { get; } = AwayTitle;
    private const string PeriodTitle = "PERIOD";
    public string PeriodTitleProperty { get; } = PeriodTitle;

    public MainViewModel()
    {
        HomeCommit = (value) =>
        {
            Console.WriteLine("runn " + value);
            _prevHomeScore.Push(HomeScore);
            HomeScore = value;
        };

        AwayCommit = (value) =>
        {
            _prevAwayScore.Push(AwayScore);
            AwayScore = value;
        };

        PeriodCommit = (value) => Period = value;
    }
}