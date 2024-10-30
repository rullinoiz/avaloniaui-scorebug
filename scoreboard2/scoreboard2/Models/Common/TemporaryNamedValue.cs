using scoreboard2.RemoteControl.Attributes;

namespace scoreboard2.Models.Common;

[ReplicatorSyncIgnore]
public class TemporaryNamedValue(string name, int clearValue = 0, int initial = 0) : NamedValue(name, initial)
{
    public void Clear() => Value = clearValue;
}