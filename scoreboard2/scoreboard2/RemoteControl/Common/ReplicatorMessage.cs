using System.Collections.Generic;

namespace scoreboard2.RemoteControl.Common;

public record ReplicatorMessage(ReplicatorSignal Signal, ReplicatorEntries? Entries)
{
    public readonly ReplicatorSignal Signal = Signal;
    public readonly ReplicatorEntries? Entries = Entries;
}

public enum ReplicatorSignal
{
    Change,
    Sync
}

public class ReplicatorEntries : Dictionary<string, object>;