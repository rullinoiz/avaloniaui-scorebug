using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models.Baseball;
using scoreboard2.Models.Common;

namespace scoreboard2.Models;

public partial class Game(GameSettings? settings = null) : ObservableObject
{
    public GameSettings Settings { get; } = settings ?? new GameSettings();
    
    public Score HomeScore { get; } = new("HOME");
    public Score AwayScore { get; } = new("AWAY");

    [ObservableProperty] private bool _homeAtBat = true;
    
    public TemporaryNamedValue Outs { get; } = new("OUTS");
    public TemporaryNamedValue Balls { get; } = new("BALLS");
    public TemporaryNamedValue Strikes { get; } = new("STRIKES");

    public Inning Period { get; } = new();
    public Base Base { get; } = new();
}