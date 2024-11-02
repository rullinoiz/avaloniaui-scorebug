using System.Xml.Serialization;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using scoreboard2.RemoteControl.Attributes;

// using Newtonsoft.Json;
// using System;
// using System.ComponentModel;
// using System.Reflection;
// using System.Runtime.Serialization;

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
    [property: ReplicatorSyncIgnore]
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