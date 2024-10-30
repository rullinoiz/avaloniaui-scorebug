using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using CommunityToolkit.Mvvm.ComponentModel;

namespace scoreboard2.RemoteControl.WebSocketPlatform;

// ReSharper disable once InconsistentNaming
/// <summary>
/// Uses JavaScript's built in WebSockets because .NET WASM doesn't support WebSocketSharp's System.Net.Sockets backend
///
/// See ./scripts/sockets.js in the scoreboard2.Browser project for implementations
/// </summary>
[SupportedOSPlatform("browser")]
public sealed partial class WebSocketShimJS : ObservableObject, ISocketShim
{
    [ObservableProperty] private bool _connected;

    public static readonly WebSocketShimJS Instance = new ();
    
    void ISocketShim.ConfigureSocket(string url) => ConfigureSocketJs(url);
    void ISocketShim.Send(string data) => SendJs(data);
    void ISocketShim.Close() => CloseJs();
    
    #region JSImports
    [JSImport("SetupWebsocket", "sockets")] 
    private static partial void ConfigureSocketJs([JSMarshalAs<JSType.String>] string url);

    [JSImport("Send", "sockets")]
    private static partial void SendJs([JSMarshalAs<JSType.String>] string data);

    [JSImport("Close", "sockets")]
    private static partial void CloseJs();
    #endregion
    
    #region JSExports
    [JSExport] private static void SocketOnMessage([JSMarshalAs<JSType.String>] string data) => ReplicatorService.Instance.SocketOnMessage(null, data);
    [JSExport] private static void SocketOnOpen() => ReplicatorService.SocketOnOpen();
    [JSExport] private static void SocketOnClose() => ReplicatorService.SocketOnClose();
    #endregion
    
    static WebSocketShimJS()
    {
        _ = JSHost.ImportAsync("sockets", "../sockets.js");
    }

    private WebSocketShimJS() { }
}