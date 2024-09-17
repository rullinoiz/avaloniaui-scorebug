using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WebSocketSharp;

namespace scoreboard2.RemoteControl.WebSocketPlatform;

public partial class WebSocketShimNative : ObservableObject, ISocketShim
{
    [ObservableProperty] private bool _connected;

    public static readonly WebSocketShimNative Instance = new ();
    
    public void ConfigureSocket(string url)
    {
        Connected = false;
        _socket?.Close();
        _socket = new WebSocket(url);
        Console.WriteLine("now trying to connect to " + url);

        _socket.OnMessage += (sender, args) => ReplicatorService.Instance.SocketOnMessage(sender, args.Data);
        _socket.OnOpen += ReplicatorService.SocketOnOpen;
        _socket.OnClose += ReplicatorService.SocketOnClose;
        
        _socket.Connect();
    }

    public void Send(string data) => _socket?.Send(data);
    public void Close() => _socket?.Close();
    
    private WebSocket? _socket;
    private WebSocketShimNative() { }
}