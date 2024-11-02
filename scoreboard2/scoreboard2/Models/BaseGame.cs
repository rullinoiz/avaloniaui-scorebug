using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.RemoteControl.Attributes;

namespace scoreboard2.Models;

public class BaseGame(GameSettings? settings = null) : ObservableObject
{
    public GameSettings Settings { get; } = settings ?? new GameSettings();
    
    public Score HomeScore { get; } = new("HOME");
    public Score AwayScore { get; } = new("AWAY");
}