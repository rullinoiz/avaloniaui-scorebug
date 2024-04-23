namespace scoreboard2.Models.Common.Interface;

public interface IGame
{
    public GameSettings Settings { get; }
    
    public Score HomeScore { get; }
    public Score AwayScore { get; }
}