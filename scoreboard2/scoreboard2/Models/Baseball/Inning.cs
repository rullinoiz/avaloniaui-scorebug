using System;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Models.Common;
using scoreboard2.Models.Common.Interface;

namespace scoreboard2.Models.Baseball;

public class InningType(int value)
{
    private const int BottomValue = 0;
    private const int TopValue = 1;
    
    public static readonly InningType Bottom = BottomValue;
    public static readonly InningType Top = TopValue;

    public readonly int Value = value;

    public override string ToString() => Value switch
        {
            TopValue => "⬆",
            BottomValue => "⬇",
            _ => throw new NotSupportedException("Value is not Top or Bottom")
        };

    public static implicit operator InningType(int t) => new (t);
    public static implicit operator int(InningType t) => t.Value;

    public static bool operator ==(InningType a, InningType b) => a.Value == b.Value;
    public static bool operator !=(InningType a, InningType b) => a.Value != b.Value;
}

public class Inning : ObservableObject, IPeriod
{
    private static readonly string[] Suffixes = ["ST", "ND", "RD", "TH"];

    private int _inningNum;
    private int _value;
    
    public string Name { get; }

    public int InningNum => _inningNum;
    
    public string InningString => $"{InningNum}{GetSuffix()}";

    public InningType TopOrBottom => Value % 2;
    
    // override Value so InningNum is also updated
    public int Value
    {
        get => _value;
        set
        {
            SetProperty(ref _value, value);
            SetProperty(ref _inningNum, (value - 1) / 2 + 1, nameof(InningNum));
            OnPropertyChanging(nameof(InningString));
            OnPropertyChanged(nameof(InningString));
            OnPropertyChanging(nameof(TopOrBottom));
            OnPropertyChanged(nameof(TopOrBottom));
        }
    }
    
    public string GetSuffix()
    {
        var i = InningNum;
        return i - 1 >= Suffixes.Length ? Suffixes.Last() : Suffixes[i - 1];
    }
    
    public Tuple<InningType, int> GetInning() => new(Value % 2, InningNum);
    
    public string ToStringWithoutOrientation() => $"{InningNum}{GetSuffix()}";
    public override string ToString() => $"{(InningType)(Value % 2)} {ToStringWithoutOrientation()}";

    protected override void OnPropertyChanged(PropertyChangedEventArgs args)
    {
        base.OnPropertyChanged(args);
    }

    public void Commit(int value)
    {
        Value = value;
    }
    
    public Inning(string name = "INNING")
    {
        Value = 1;
        Name = name;
    }
}

