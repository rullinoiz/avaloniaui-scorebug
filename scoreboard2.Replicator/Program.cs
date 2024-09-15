using WebSocketSharp.Server;

namespace scoreboard2.Replicator;

#pragma warning disable CS0618 // Type or member is obsolete

internal sealed class Program
{
    public static Task Main() => MainAsync();

    private static async Task MainAsync()
    {
        var server = new WebSocketServer(25565);
        
        server.AddWebSocketService("/replicator", () => new Replicator
        {
            IgnoreExtensions = true
        });
        
        server.Start();

        await Task.Delay(-1);
    }
}