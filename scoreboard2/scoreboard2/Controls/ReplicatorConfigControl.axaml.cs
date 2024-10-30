using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Primitives;
using Newtonsoft.Json;
using scoreboard2.RemoteControl;
using scoreboard2.RemoteControl.Common;
using scoreboard2.RemoteControl.WebSocketPlatform;

namespace scoreboard2.Controls;

public class ReplicatorConfigControl : TemplatedControl
{
    public static readonly StyledProperty<ISocketShim> ShimProperty = AvaloniaProperty.Register<ReplicatorConfigControl, ISocketShim>(
        "Shim");

    public ISocketShim Shim
    {
        get => GetValue(ShimProperty);
        set => SetValue(ShimProperty, value);
    }

    public static readonly StyledProperty<string> ReplicatorUrlProperty = AvaloniaProperty.Register<ReplicatorConfigControl, string>(
        "ReplicatorUrl");

    public string ReplicatorUrl
    {
        get => GetValue(ReplicatorUrlProperty);
        set => SetValue(ReplicatorUrlProperty, value);
    }

    public Task SyncClickButton()
    {
        var message = new ReplicatorMessage("sendMessage", ReplicatorSignal.Sync);
        ReplicatorService.Instance.Send(JsonConvert.SerializeObject(message));
        return Task.CompletedTask;
    }
}