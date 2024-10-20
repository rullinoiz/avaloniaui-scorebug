using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp.Server;

namespace scoreboard2.Replicator;

#pragma warning disable CS0618 // Type or member is obsolete

internal static class Program
{
    public static Task Main(string[] args) => MainAsync(args);

    private static async Task MainAsync(string[] args)
    {
        WebSocketServer? server;
        if (args.Length == 2)
        {
            server = new WebSocketServer(25565, true)
            {
                SslConfiguration =
                {
                    ServerCertificate = X509Certificate2.CreateFromPemFile(args[0], args[1]),
                    EnabledSslProtocols = SslProtocols.Tls12,
                    ClientCertificateRequired = false,
                    CheckCertificateRevocation = false
                }
            };
        }
        else
        {
            server = new WebSocketServer(25565);
        }
        
        server.AddWebSocketService("/ws", () => new Replicator
        {
            IgnoreExtensions = true
        });
        
        server.Start();
        Console.WriteLine("replicator started, listening on port " + server.Port);

        await Task.Delay(-1);
    }
}