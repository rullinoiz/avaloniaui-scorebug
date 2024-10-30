using System;
using System.Security.Authentication;
using CommunityToolkit.Mvvm.ComponentModel;
using WebSocketSharp;

namespace scoreboard2.RemoteControl.WebSocketPlatform;

public sealed partial class WebSocketShimNative : ObservableObject, ISocketShim
{
    [ObservableProperty] private bool _connected;

    public static readonly WebSocketShimNative Instance = new();
    
    void ISocketShim.ConfigureSocket(string url)
    {
        Connected = false;
        _socket?.Close();
        Console.WriteLine("now trying to connect to " + url);
        try
        {
            _socket = new WebSocket(url);
        }
        catch (Exception e) when (e is ArgumentException or InvalidOperationException)
        {
            return; 
        }
        _socket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
        _socket.SslConfiguration.CheckCertificateRevocation = false;

        _socket.OnMessage += (sender, args) => ReplicatorService.Instance.SocketOnMessage(sender, args.Data);
        _socket.OnOpen += ReplicatorService.SocketOnOpen;
        _socket.OnClose += ReplicatorService.SocketOnClose;
        
        _socket.Connect();
    }

    void ISocketShim.Send(string data)
    {
        if (_socket?.ReadyState == WebSocketState.Open)
        {
            _socket?.Send(data);
        }
    }

    void ISocketShim.Close() => _socket?.Close();
    
    private WebSocket? _socket;
    private WebSocketShimNative() { }
}