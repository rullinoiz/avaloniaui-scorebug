using WebSocketSharp;
using WebSocketSharp.Server;

namespace scoreboard2.Replicator;

public sealed class Replicator : WebSocketBehavior
{
    private void SendToAllBut(string id, string data)
    {
        foreach (var session in Sessions.Sessions)
        {
            if (session.ID != id)
            {
                Sessions.SendTo(data, session.ID);
            }
        }
    }
    
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        SendToAllBut(ID, e.Data);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        
        Console.WriteLine("new client " + ID);
    }
    
    
}