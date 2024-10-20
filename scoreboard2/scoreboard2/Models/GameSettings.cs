using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using scoreboard2.RemoteControl.Attributes;

#pragma warning disable CS0657

namespace scoreboard2.Models;

// [Serializable]
public partial class GameSettings : ObservableObject
{
    private static readonly GameSettings Default = new ();
    
    [ObservableProperty] 
    [property: ReplicatorIgnore]
    [property: XmlIgnore]
    // [property: JsonIgnore]
    private Bitmap? _homeImage;
    
    [ObservableProperty]
    // [property: DefaultValue(Colors.Black)]
    private Color _homeBackgroundColor = Colors.Black;
    [ObservableProperty]
    // [property: DefaultValue(Colors.White)]
    private Color _homeForegroundColor = Colors.White;
    [ObservableProperty]
    // [property: DefaultValue(string.Empty)]
    private string _homeCityName = string.Empty;
    [ObservableProperty]
    // [property: DefaultValue(string.Empty)]
    private string _homeTeamName = string.Empty;
    
    [ObservableProperty]
    [property: ReplicatorIgnore]
    [property: XmlIgnore]
    // [property: JsonIgnore]
    private Bitmap? _awayImage;
    
    [ObservableProperty]
    // [property: DefaultValue(Colors.Black)]
    private Color _awayBackgroundColor = Colors.Black;
    [ObservableProperty]
    // [property: DefaultValue(Colors.White)]
    private Color _awayForegroundColor = Colors.White;
    [ObservableProperty]
    // [property: DefaultValue(string.Empty)]
    private string _awayCityName = string.Empty;
    [ObservableProperty]
    // [property: DefaultValue(string.Empty)]
    private string _awayTeamName = string.Empty;
    
    [ObservableProperty]
    [property: ReplicatorIgnore]
    // [property: DefaultValue(string.Empty)]
    // [property: XmlIgnore]
    // [property: JsonIgnore]
    private string _replicatorUrl = string.Empty;

    public void Load(GameSettings copy)
    {
        var classType = typeof(GameSettings);
        var properties = classType.GetProperties();

        foreach (var property in properties)
        {
            var copyPropertyValue = property.GetValue(copy);
            var defaultPropertyValue = property.GetValue(Default);
            
            // so we don't override existing properties and so we can have presets that don't interfere with each other
            if (copyPropertyValue == defaultPropertyValue) 
                continue;
            
            property.SetValue(this, copyPropertyValue);
        }
    }
}

// public partial class GameSettings
// {
//     public void GetObjectData(SerializationInfo info, StreamingContext context)
//     {
//         info.AddValue(nameof(HomeBackgroundColor), HomeBackgroundColor);
//         info.AddValue(nameof(HomeForegroundColor), HomeForegroundColor);
//         info.AddValue(nameof(HomeCityName), HomeCityName);
//         info.AddValue(nameof(HomeTeamName), HomeTeamName);
//         
//         info.AddValue(nameof(AwayBackgroundColor), AwayBackgroundColor);
//         info.AddValue(nameof(AwayForegroundColor), AwayForegroundColor);
//         info.AddValue(nameof(AwayCityName), AwayCityName);
//         info.AddValue(nameof(AwayTeamName), AwayTeamName);
//     }
//
//     public GameSettings(SerializationInfo info, StreamingContext context)
//     {
//         HomeBackgroundColor = (Color)info.GetValue(nameof(HomeBackgroundColor), typeof(Color))!;
//         HomeForegroundColor = (Color)info.GetValue(nameof(HomeForegroundColor), typeof(Color))!;
//         HomeCityName = (string)info.GetValue(nameof(HomeCityName), typeof(string))!;
//         HomeTeamName = (string)info.GetValue(nameof(HomeTeamName), typeof(string))!;
//         
//         AwayBackgroundColor = (Color)info.GetValue(nameof(AwayBackgroundColor), typeof(Color))!;
//         AwayForegroundColor = (Color)info.GetValue(nameof(AwayForegroundColor), typeof(Color))!;
//         AwayCityName = (string)info.GetValue(nameof(AwayCityName), typeof(string))!;
//         AwayTeamName = (string)info.GetValue(nameof(AwayTeamName), typeof(string))!;
//     }
//     
//     public GameSettings() { }
// }