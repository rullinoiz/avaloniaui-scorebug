using System;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models;
using scoreboard2.Models.Baseball;
using scoreboard2.Models.Basketball;
using scoreboard2.Models.Football;
using scoreboard2.ViewModels.Common;

#if !(BROWSER || IOS || ANDROID)
using scoreboard2.Windows;
using scoreboard2.Common;
using System.Collections.Generic;
#endif

namespace scoreboard2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public BaseGame BaseGame { get; } = new();
    public BaseballModel Baseball { get; } = new();
    public FootballModel Football { get; } = new();
    public BasketballModel Basketball { get; } = new();
    
    [ObservableProperty] private int _selectedGame;
    
#if !(BROWSER || IOS || ANDROID)
    private readonly List<ScoreboardWindow> _scoreboardWindows = [];
#endif

#if !(BROWSER || IOS || ANDROID)
    public void AddNewScorebugView(IScorebugView view)
    {
        var window = new ScoreboardWindow(this, view);
        _scoreboardWindows.Add(window);
        window.Closed += (_, _) =>
        {
            _scoreboardWindows.Remove(window);
        };
        window.Show();
    }
#endif
    
    partial void OnSelectedGameChanged(int value)
    {
        SelectedGame = value;
#if !(BROWSER || IOS || ANDROID)
        foreach (var window in _scoreboardWindows.ToArray())
        {
            window.Close();
        }
        _scoreboardWindows.Clear();
#endif
    }
    
    public MainViewModel()
    {
        Console.WriteLine("instantiating main view model");
        
#if !(BROWSER || IOS || ANDROID)
        var scorebugConfig = new ScorebugConfig(this);
        scorebugConfig.Show();
#endif
        
        OnSelectedGameChanged(SelectedGame);
    }
}