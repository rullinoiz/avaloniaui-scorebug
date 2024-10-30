namespace scoreboard2.RemoteControl.Common;

public record ReplicatorMessage(string action, ReplicatorSignal Signal, ReplicatorEntries? Entries = null)
{
    // ReSharper disable once InconsistentNaming
    public readonly string action = action;  // this is required so that AWS plays nice (i think)
    public readonly ReplicatorSignal Signal = Signal;
    public readonly ReplicatorEntries? Entries = Entries;
}