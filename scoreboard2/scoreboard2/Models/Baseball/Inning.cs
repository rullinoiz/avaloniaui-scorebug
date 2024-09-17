using System;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.RemoteControl.Attributes;

namespace scoreboard2.Models.Baseball;

#pragma warning disable CS0660
#pragma warning disable CS0661
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

public class Inning : ObservableObject
{
    private static readonly string[] Suffixes = ["st", "nd", "rd", "th"];

    private int _inningNum;
    private int _value;
    
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Name { get; }

    [ReplicatorIgnore]
    public int InningNum => _inningNum;
    
    [ReplicatorIgnore]
    public string InningString => ToString();

    [ReplicatorIgnore]
    public string PeriodString => $"{Value}{Suffixes.GetValue(_value - 1) ?? Suffixes.Last()}";
    
    [ReplicatorIgnore]
    public InningType TopOrBottom => Value % 2;
    
    // override Value so InningNum is also updated
#pragma warning disable MVVMTK0034
    public int Value
    {
        get => _value;
        set
        {
            SetProperty(ref _value, value);
            SetProperty(ref _inningNum, (value - 1) / 2 + 1, nameof(InningNum));
            OnPropertyChanging(nameof(InningString));
            OnPropertyChanged(nameof(InningString));
            OnPropertyChanging(nameof(PeriodString));
            OnPropertyChanged(nameof(PeriodString));
            OnPropertyChanging(nameof(TopOrBottom));
            OnPropertyChanged(nameof(TopOrBottom));
        }
    }
#pragma warning restore MVVMTK0034
    public string GetSuffix(int? i = null)
    {
        i ??= InningNum;
        return i - 1 >= Suffixes.Length ? Suffixes.Last() : Suffixes[i.Value - 1];
    }
    
    public Tuple<InningType, int> GetInning() => new(Value % 2, InningNum);
    
    public string ToStringWithoutOrientation() => $"{InningNum}{GetSuffix()}";
    public override string ToString() => $"{(InningType)(Value % 2)} {ToStringWithoutOrientation()}";

    protected override void OnPropertyChanged(PropertyChangedEventArgs args)
    {
        base.OnPropertyChanged(args);
    }
    
    public Inning(string name = "INNING")
    {
        Value = 1;
        Name = name;
    }
}

