using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models.Baseball;

public class Base
{
    public BaseState Base1 { get; } = false;
    public BaseState Base2 { get; } = false;
    public BaseState Base3 { get; } = false;
    
    public void Toggle(int b)
    {
        switch (b)
        {
            case 0: Base1.Toggle(); break;
            case 1: Base2.Toggle(); break;
            case 2: Base3.Toggle(); break;
        }
    }
}

public partial class BaseState(bool state) : ObservableObject
{
    [ObservableProperty] private bool _state = state;

    public void Toggle() => State ^= true;

    public override string ToString() => State.ToString();
    public static implicit operator BaseState(bool e) => new(e);
}