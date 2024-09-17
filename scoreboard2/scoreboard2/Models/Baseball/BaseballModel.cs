using scoreboard2.Models.Common;

namespace scoreboard2.Models.Baseball;

public class BaseballModel : ExtraModelBase
{
    public TemporaryNamedValue Outs { get; } = new("OUTS");
    public TemporaryNamedValue Balls { get; } = new("BALLS");
    public TemporaryNamedValue Strikes { get; } = new("STRIKES");

    public Base Base { get; } = new();
    
    public Inning Inning { get; } = new();
}