using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models.Common;

namespace scoreboard2.Models.Football;

public partial class FootballModel : ExtraModelBase
{
    public TemporaryNamedValue HomeTimeouts { get; } = new("HOME TIMEOUTS", initial: 3);
    public TemporaryNamedValue AwayTimeouts { get; } = new("AWAY TIMEOUTS", initial: 3);

    [ObservableProperty] private bool _homeDefending;

    public TemporaryNamedValue Down { get; } = new("DOWN", initial: 1, clearValue: 1);
    public TemporaryNamedValue Yards { get; } = new("YARDS", initial: 10, clearValue: 10);
}