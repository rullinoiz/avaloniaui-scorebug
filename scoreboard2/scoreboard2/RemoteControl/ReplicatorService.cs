#if BROWSER
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using WebSocketSharp;

namespace scoreboard2.RemoteControl;

#if BROWSER
[SupportedOSPlatform("browser")]
#endif
public partial class ReplicatorService : ObservableObject
{
    public static ReplicatorService Instance { get; private set; }

    static ReplicatorService()
    {
        Instance = new ReplicatorService();
#if BROWSER
        var path = "../sockets.js";
        Console.WriteLine("trying " + path);
        _ = JSHost.ImportAsync("sockets", path);
        // JSHost.ImportAsync("Send", "./main.js").RunSynchronously();
        // JSHost.ImportAsync("Close", "./main.js").RunSynchronously();
#endif
    }

    [ObservableProperty] private bool _connected;
    
    #region private

    private object? _viewModel;
    private record RegisteredPropertyInfo(string Path, List<string> Properties);
    private readonly Dictionary<object, RegisteredPropertyInfo> DebouncePropertyList = [];
    private readonly string[] BlacklistedProperties = ["ReplicatorUrl", "HomeImage", "AwayImage", "HomeBackgroundColor", "HomeForegroundColor", "AwayBackgroundColor", "AwayForegroundColor"];

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
        ? (info.Path.IsNullOrEmpty() ? propertyName : $"{info.Path}.{propertyName}") : string.Empty;
    #endregion
    private void PropertyChangedInput(object? sender, PropertyChangedEventArgs args)
    {
        if (BlacklistedProperties.Contains(args.PropertyName)) return;
        
        RegisteredPropertyInfo? info = null;
        if (sender is not null && DebouncePropertyList.TryGetValue(sender, out info) 
                               && info.Properties.Contains(args.PropertyName!))
        {
            info.Properties.Remove(args.PropertyName!);
            return;
        }
        
        var val = sender?.GetType().GetProperty(args.PropertyName!)?.GetValue(sender);
        Console.WriteLine($"{info?.Path + "." + args.PropertyName} => ({val?.GetType()}) {val}");

        if (Property2String(sender!, args.PropertyName!) is not { } fullPath || val is null) return;
        var data = new Dictionary<string, object> { {fullPath, val} };
        var serializedData = JsonConvert.SerializeObject(data);
        Console.WriteLine("sending data => " + serializedData);
        Send(serializedData);
    }
    
    public void RegisterProperties(object o, PropertyInfo[]? properties = null, string? path = null)
    {
        properties ??= o.GetType().GetProperties();

        if (o is ObservableObject oo)
        {
            DebouncePropertyList.Add(oo, new RegisteredPropertyInfo(path ?? string.Empty, []));
            oo.PropertyChanged += PropertyChangedInput;
            Console.WriteLine($"{oo.GetType().Name} is observableobject");
        }
        else return;

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
#if !BROWSER
    [ObservableProperty] private WebSocket? _socket;
    
    public void ConfigureSocket(string url)
    {
        Connected = false;
        Socket?.Close();
        Socket = new WebSocket(url);

        Socket.OnMessage += (sender, args) => SocketOnMessage(sender, args.Data);

        Socket.OnOpen += SocketOnOpen;

        Socket.OnClose += SocketOnClose;
        
        Socket.Connect();
    }

    public void Send(string data) => Socket?.Send(data);
    public void Close() => Socket?.Close();
#endif
    
#if BROWSER
    public void ConfigureSocket(string url) => ConfigureSocketJS(url);

    [JSImport("SetupWebsocket", "sockets")] 
    public static partial void ConfigureSocketJS([JSMarshalAs<JSType.String>] string url);

    [JSImport("Send", "sockets")]
    public static partial void Send([JSMarshalAs<JSType.String>] string data);

    [JSImport("Close", "sockets")]
    public static partial void Close();

    [JSExport] private static void SocketOnMessage([JSMarshalAs<JSType.String>] string data) => Instance.SocketOnMessage(null, data);
    [JSExport] private static void SocketOnOpen() => SocketOnOpen(null, null);
    [JSExport] private static void SocketOnClose() => SocketOnClose(null, null);
#endif
    
    private static void SocketOnOpen(object? _ = null, EventArgs? args = null)
    {
        Console.WriteLine("now connected " + args);
        Instance.Connected = true;
    }

    private static void SocketOnClose(object? _ = null, EventArgs? args = null)
    {
        Console.WriteLine("now disconnected " + args);
        Instance.Connected = false;
    }
    
    private void SocketOnMessage(object? _, string raw)
    {
        Console.WriteLine("new data => " + raw);
        if (JsonConvert.DeserializeObject<Dictionary<string, object>>(raw) is not { } data) return;

        foreach (var property in data)
        {
            if (GetObjectFromPath(property.Key) is not { } o) continue;
            var propertyName = property.Key.Split(".").Last();
            if (GetProperty(o, propertyName) is not { } propertyInfo) continue;

            if (!DebouncePropertyList.TryGetValue(o, out var r)) continue;
            r.Properties.Add(propertyName);
            object? val = null;
            if (property.Value is long l) val = (int)l;
            Dispatcher.UIThread.Invoke(
                () => { try { propertyInfo.SetValue(o, val ?? property.Value); } catch (ArgumentException) { } });
        }
    }
}