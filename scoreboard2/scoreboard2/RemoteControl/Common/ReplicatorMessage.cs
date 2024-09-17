using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace scoreboard2.RemoteControl.Common;

[Serializable]
public record ReplicatorMessage(ReplicatorSignal Signal, ReplicatorEntries? Entries)
    : ISerializable
{
    public ReplicatorSignal Signal = Signal;
    public ReplicatorEntries? Entries = Entries;
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Signal), Signal);
        info.AddValue(nameof(Entries), Entries);
    }

    public ReplicatorMessage(SerializationInfo info, StreamingContext context) 
        : this((ReplicatorSignal)info.GetValue(nameof(Signal), typeof(ReplicatorSignal))!, 
            (ReplicatorEntries)info.GetValue(nameof(Entries), typeof(ReplicatorEntries))!)
    {
    }
}

public enum ReplicatorSignal
{
    Change,
    Sync
}

public class ReplicatorEntries : Dictionary<string, object>;