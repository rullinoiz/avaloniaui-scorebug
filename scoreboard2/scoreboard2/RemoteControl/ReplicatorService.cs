using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using scoreboard2.RemoteControl.Attributes;
using scoreboard2.RemoteControl.Common;
using scoreboard2.RemoteControl.WebSocketPlatform;

namespace scoreboard2.RemoteControl;

public partial class ReplicatorService : ObservableObject
{
    public static ReplicatorService Instance { get; private set; }
    public static ISocketShim Shim { get; }
    public record RegisteredPropertyInfo(string Path, List<string> Properties);
    private readonly Dictionary<object, RegisteredPropertyInfo> DebouncePropertyList = [];

    static ReplicatorService()
    {
        Instance = new ReplicatorService();
        
        // WebSocketSharp uses System.Net.Sockets which is NOT supported by .NET WASM
        // this lets us swap out implementations for one that shims JavaScript's native WebSocket
        Shim = OperatingSystem.IsBrowser() ? WebSocketShimJS.Instance : WebSocketShimNative.Instance;
    }
    
    #region private

    private object? _viewModel;
    private readonly string[] _blacklistedProperties = [];
    private object? GetObjectFromPath(string path)
    {
        var properties = path.Split(".").ToList();
        var current = _viewModel;

        while (properties.Count > 0)
        {
            if (properties.Count == 1) return current;
            if (current is null) return null;
            current = GetProperty(current, properties.First())?.GetValue(current)!;
            properties.RemoveAt(0);
        }

        return null;
    }
    
    private static PropertyInfo? GetProperty(object o, string propertyName) 
        => o.GetType().GetProperty(propertyName);

    private string Property2String(object o, string propertyName) => DebouncePropertyList.TryGetValue(o, out var info) 
        ? (string.IsNullOrEmpty(info.Path) ? propertyName : $"{info.Path}.{propertyName}") : string.Empty;
    #endregion
    private void PropertyChangedInput(object? sender, PropertyChangedEventArgs args)
    {
        if (_blacklistedProperties.Contains(args.PropertyName) || !Shim.Connected) return;
        
        RegisteredPropertyInfo? info = null;
        if (sender is not null && DebouncePropertyList.TryGetValue(sender, out info) 
                               && info.Properties.Contains(args.PropertyName!))
        {
            info.Properties.Remove(args.PropertyName!);
            return;
        }
        
        var property = sender?.GetType().GetProperty(args.PropertyName!);
        if (property?.GetCustomAttribute<ReplicatorIgnoreAttribute>() is not null) return;
        var val = property?.GetValue(sender);
        Console.WriteLine($"{info?.Path + "." + args.PropertyName} => ({val?.GetType()}) {val}");

        if (Property2String(sender!, args.PropertyName!) is not { } fullPath || val is null) return;
        var data = new ReplicatorMessage("sendMessage", ReplicatorSignal.Change, new ReplicatorEntries { {fullPath, val} });
        var serializedData = JsonConvert.SerializeObject(data);
        Send(serializedData);
    }
    
    public void RegisterProperties(object o, PropertyInfo[]? properties = null, string? path = null)
    {
        properties ??= o.GetType().GetProperties();

        if (o is ObservableObject oo)
        {
            DebouncePropertyList.Add(oo, new RegisteredPropertyInfo(path ?? string.Empty, []));
            oo.PropertyChanged += PropertyChangedInput;
        }
        else
        {
            switch (o)
            {
                case string _:
                case int _:
                case byte _:
                case bool _:
                    return;
            }
        }

        foreach (var p in properties)
        {
            var cls = p.GetValue(o);
            if (cls is null) return;
            
            var newPath = path is null ? p.Name : $"{path}.{p.Name}";
            
            RegisterProperties(cls, cls.GetType().GetProperties(), newPath);
            Console.WriteLine($"{newPath} = {cls.GetType().Name}");
        }
    }

    public void Setup(object vm) => _viewModel = vm;
}

public partial class ReplicatorService
{
    [ObservableProperty] private bool _isHost;
    
    public void ConfigureSocket(string url) => Shim.ConfigureSocket(url);

    public void Send(string data) => Shim.Send(data);
    public void Close() => Shim.Close();
    
    public static void SocketOnOpen(object? _ = null, EventArgs? args = null)
    {
        Console.WriteLine("now connected");
        Shim.Connected = true;
    }

    public static void SocketOnClose(object? _ = null, EventArgs? args = null)
    {
        Console.WriteLine("now disconnected");
        Shim.Connected = false;
    }
    
    public void SocketOnMessage(object? _, string raw)
    {
        Console.WriteLine("new data => " + raw);
        if (JsonConvert.DeserializeObject<ReplicatorMessage>(raw) is not var (_, signal, data)) return;
        if (signal == ReplicatorSignal.Sync)
        {
            SocketOnSync();
            return;
        }
        
        if (signal != ReplicatorSignal.Change) return;

        foreach (var property in data!)
        {
            if (GetObjectFromPath(property.Key) is not { } o) continue;
            var propertyName = property.Key.Split(".").Last();
            if (GetProperty(o, propertyName) is not { } propertyInfo) continue;

            if (!DebouncePropertyList.TryGetValue(o, out var r)) continue;
            r.Properties.Add(propertyName);

            object? val;
            switch (propertyInfo.PropertyType)
            {
                case { } t when t == typeof(int):
                    val = Convert.ToInt32(property.Value);
                    break;
                case { } t when t == typeof(Avalonia.Media.Imaging.Bitmap): // don't even try interpreting images
                    return;
                default:
                    if (propertyInfo.PropertyType == property.Value.GetType())
                    {
                        val = property.Value;
                        break;
                    }
                    val = ((JObject)property.Value).ToObject(propertyInfo.PropertyType);
                    break;
            }
            Dispatcher.UIThread.Invoke(
                () => { try { propertyInfo.SetValue(o, val ?? property.Value); } catch (ArgumentException) { } }
            );
        }
    }

    private void SocketOnSync()
    {
        if (!IsHost) return;
        
        var entries = new ReplicatorEntries();

        foreach (var (o, (_, _)) in DebouncePropertyList)
        {
            foreach (var property in o.GetType().GetProperties())
            {
                if (property.GetCustomAttribute<ReplicatorIgnoreAttribute>() is not null) continue;
                if (property.GetValue(o)?.GetType().GetCustomAttribute<ReplicatorSyncIgnoreAttribute>() is not null) continue;
                
                entries.Add(Property2String(o, property.Name), property.GetValue(o)!);
            }
        }
        
        Shim.Send(JsonConvert.SerializeObject(new ReplicatorMessage("sendMessage", ReplicatorSignal.Change, entries)));
    }
}