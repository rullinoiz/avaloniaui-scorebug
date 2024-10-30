using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.RemoteControl.Attributes;

namespace scoreboard2.Models.Common;

[ReplicatorSyncIgnore]
public partial class NamedValue(string? name = null, int initial = 0) : ObservableObject
{
    [ReplicatorIgnore] public string Name { get; } = name ?? string.Empty;
    // ReSharper disable once InconsistentNaming
    [ObservableProperty] private int _value = initial;
}