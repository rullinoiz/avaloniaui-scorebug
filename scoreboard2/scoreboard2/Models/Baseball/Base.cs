using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models.Baseball;

public partial class Base : ObservableObject
{
    public const int Bases = 3;

    [ObservableProperty] private ObservableCollection<BaseState> _baseStates;
    
    public void Toggle(int b)
    {
        BaseStates[b].Toggle();
        OnPropertyChanged();
    }

    public Base()
    {
        BaseStates = [false, false, false];
    }
}

public partial class BaseState(bool state) : ObservableObject
{
    [ObservableProperty] private bool _state = state;

    public void Toggle() => State ^= true;

    public override string ToString() => State.ToString();
    public static implicit operator bool(BaseState e) => e.State;
    public static implicit operator BaseState(bool e) => new(e);
}