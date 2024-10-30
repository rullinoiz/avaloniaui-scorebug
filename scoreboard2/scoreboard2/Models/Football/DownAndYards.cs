using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.Converters;
using scoreboard2.RemoteControl.Attributes;
#pragma warning disable CS0657

namespace scoreboard2.Models.Football;

[ReplicatorSyncIgnore]
public partial class DownAndYards : ObservableObject
{
    [ObservableProperty] 
    [property: ReplicatorIgnore]
    [property: ReplicatorSyncIgnore]
    private string _output = string.Empty;
    
    [ObservableProperty] private bool _long;
    [ObservableProperty] private bool _goal;
    [ObservableProperty] private bool _inches;
    
    [ObservableProperty] private int _down;
    [ObservableProperty] private int _yards = 10;

    public DownAndYards()
    {
        OnPropertyChanged();         // to update Output
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        
        // Goal and Long should not both be true
        switch (e.PropertyName)
        {
            case nameof(Goal):
                // Long ^= Goal;
                if (Goal && (Long || Inches)) Long = Inches = false;
                break;
            case nameof(Long):
                // Goal ^= Long;
                if ((Goal || Inches) && Long) Goal = Inches = false;
                break;
            case nameof(Inches):
                if ((Goal || Long) && Inches) Long = Goal = false;
                break;
        }

        if (Down <= 0)
        {
            Output = "";
            return;
        }

        var output = $"{StringTercer.Convert(Down)} & ";
        if (Goal) output += "GOAL";
        else if (Long) output += "LONG";
        else if (Inches) output += "INCHES";
        else output += Yards;
        Output = output;
    }
}