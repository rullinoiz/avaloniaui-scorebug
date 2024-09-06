using scoreboard2.Models.Baseball;

namespace scoreboard2.Models;

public class BaseGame(GameSettings? settings = null)
{
    public GameSettings Settings { get; } = settings ?? new GameSettings();
    
    public Score HomeScore { get; } = new("HOME");
    public Score AwayScore { get; } = new("AWAY");
    public Inning Period { get; } = new();

}