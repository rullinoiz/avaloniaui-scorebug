using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.Models.Baseball;

public class InningType(int value)
{
    public const int BottomValue = 0;
    public const int TopValue = 1;
    
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

public class Inning : Period
{
    private static readonly string[] Suffixes = ["ST", "ND", "RD", "TH"];

    public string GetSuffix()
    {
        var i = Value;
        return i - 1 > Suffixes.Length ? Suffixes[-1] : Suffixes[i - 1];
    }
    
    public Tuple<InningType, int> GetInning() 
        => new(Value % 2, Value / 2);
    
    public string ToStringWithoutOrientation() => $"{Value / 2}{GetSuffix()}";
    public override string ToString() => $"{(InningType)(Value % 2)} {ToStringWithoutOrientation()}";
}

