using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models.Common;

public partial class NamedValue(string? name = null, int initial = 0) : ObservableObject
{
    public string Name { get; } = name ?? string.Empty;
    // ReSharper disable once InconsistentNaming
    [ObservableProperty] private int _value = initial;
}