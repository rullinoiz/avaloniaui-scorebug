using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models.Common;

public partial class NamedValue(string? name = null) : ObservableObject
{
    public string Name { get; } = name ?? string.Empty;
    [ObservableProperty] private int _value;
}