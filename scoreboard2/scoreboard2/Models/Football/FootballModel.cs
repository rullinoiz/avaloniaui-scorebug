using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models.Common;

namespace scoreboard2.Models.Football;

public partial class FootballModel : ExtraModelBase
{
    public TemporaryNamedValue HomeTimeouts { get; } = new("HOME T/O", initial: 3);
    public TemporaryNamedValue AwayTimeouts { get; } = new("AWAY T/O", initial: 3);

    [ObservableProperty] private bool _homeDefending;

    public DownAndYards DownAndYards { get; } = new();

    public Quarter Quarter { get; } = new ();
}