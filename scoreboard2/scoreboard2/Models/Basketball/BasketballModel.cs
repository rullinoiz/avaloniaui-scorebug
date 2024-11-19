using System;

namespace scoreboard2.Models.Basketball;

public class BasketballModel : ExtraModelBase
{
    public Quarter Quarter { get; } = new();
    public Clock QuarterClock { get; } = new(TimeSpan.FromMinutes(8));
    public Clock ShotClock { get; } = new(TimeSpan.FromSeconds(24))
    {
        IsShotClock = true
    };
}