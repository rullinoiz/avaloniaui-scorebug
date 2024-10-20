namespace scoreboard2.RemoteControl.WebSocketPlatform;

/// <summary>
/// depending on the platform we may need to use a different WebSocket implementation
/// the library i'm using relies on .NET's System.Net.Sockets which is not supported on WASM, so we use JS's.
///
/// thankfully, due to the power of design, we only need these specific methods and we can swap out implementations
/// </summary>
public interface ISocketShim
{
    /// <summary>
    /// implemented through the [ObservableProperty] attribute at compile time
    /// </summary>
    public bool Connected { get; set; }
    
    #nullable disable
    public static ISocketShim Instance;
    #nullable restore
    
    public void Send(string data);
    public void Close();
    public void ConfigureSocket(string url);
}