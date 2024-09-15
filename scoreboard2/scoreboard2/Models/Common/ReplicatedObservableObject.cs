using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.RemoteControl;

// using System.ComponentModel;
// using static scoreboard2.Common.Extensions.ObjectFirePublicEvent;

namespace scoreboard2.Models.Common;

/// <summary>
/// Declares an ObservableObject to be monitored by <see cref="ReplicatorService"/>
/// </summary>
public class ReplicatedObservableObject : ObservableObject
{
    // protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    // {
    //     this.FirePublicEvent(nameof(PropertyChanged), [e]);
    // }
}