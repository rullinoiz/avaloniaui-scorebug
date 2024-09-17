using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models.Common;
using scoreboard2.RemoteControl.Attributes;

#pragma warning disable CS0657

namespace scoreboard2.Models.Football;

public partial class FootballModel : ExtraModelBase
{
    public TemporaryNamedValue HomeTimeouts { get; } = new("HOME T/O", initial: 3);
    public TemporaryNamedValue AwayTimeouts { get; } = new("AWAY T/O", initial: 3);

    [ObservableProperty] private bool _homeDefending;

    public TemporaryNamedValue Down { get; } = new("DOWN", initial: 1, clearValue: 1);
    public TemporaryNamedValue Yards { get; } = new("YARDS", initial: 10, clearValue: 10);

    [ObservableProperty] 
    [property: ReplicatorIgnore]
    private string _output = string.Empty;

    public Quarter Quarter { get; } = new ();
}