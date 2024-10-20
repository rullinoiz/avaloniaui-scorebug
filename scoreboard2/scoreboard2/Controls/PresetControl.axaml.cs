using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Platform.Storage;
using Newtonsoft.Json;
using scoreboard2.Converters;
using scoreboard2.Models;

namespace scoreboard2.Controls;

public class PresetControl : TemplatedControl
{
    private static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings 
        {DefaultValueHandling = DefaultValueHandling.Ignore});
    
    public static readonly StyledProperty<GameSettings> GameSettingsProperty = AvaloniaProperty.Register<PresetControl, GameSettings>(
        "GameSettings");

    public GameSettings GameSettings
    {
        get => GetValue(GameSettingsProperty);
        set => SetValue(GameSettingsProperty, value);
    }

    static PresetControl()
    {
        Serializer.Converters.Add(new AvaloniaBitmapJsonConverter());
    }

    public async Task LoadClickButton()
    {
        var toplevel = TopLevel.GetTopLevel(this) ?? throw new NullReferenceException(
            "errrm something went wrong");

        var files = await toplevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open preset file",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.All }
        });

        if (files.Count < 1) return;
        
        await using var stream = await files[0].OpenReadAsync();
        using var streamReader = new StreamReader(stream);
        // var fileContents = await streamReader.ReadToEndAsync();
        
        if (Serializer.Deserialize<GameSettings>(new JsonTextReader(streamReader)) is not { } newSettings)
        {
            Console.WriteLine("could not parse preset");
            return;
        }
        
        GameSettings.Load(newSettings);
    }

    public async Task SaveClickButton()
    {
        var toplevel = TopLevel.GetTopLevel(this) ?? throw new NullReferenceException(
            "errrm something went wrong");
        
        var file = await toplevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Save preset file",
            DefaultExtension = ".sb2",
            SuggestedFileName = "Scoreboard2Preset.sb2"
        });
        
        if (file is null)
        {
            Console.WriteLine("no file specified");
            return;
        }
        
        await using var stream = await file.OpenWriteAsync();
        await using var stringWriter = new StringWriter();
        try
        {
            Serializer.Serialize(stringWriter, GameSettings);
        
            await using var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteAsync(stringWriter.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}