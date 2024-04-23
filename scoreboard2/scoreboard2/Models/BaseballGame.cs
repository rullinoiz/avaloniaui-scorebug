using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models.Baseball;
using scoreboard2.Models.Common;

namespace scoreboard2.Models;

public partial class BaseballGame(GameSettings? settings = null) : Game(settings)
{
    [ObservableProperty] private bool _homeAtBat = true;
    
    public TemporaryNamedValue Outs { get; } = new("OUTS");
    public TemporaryNamedValue Balls { get; } = new("BALLS");
    public TemporaryNamedValue Strikes { get; } = new("STRIKES");

    public new Inning Period { get; } = new();
    public Base Base { get; } = new();
}