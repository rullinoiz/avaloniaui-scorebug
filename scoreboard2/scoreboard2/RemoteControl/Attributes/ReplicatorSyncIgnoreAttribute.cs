using System;

namespace scoreboard2.RemoteControl.Attributes;

/// <summary>
/// Tells the ReplicatorService to omit a property when a Sync signal is received.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
public class ReplicatorSyncIgnoreAttribute : Attribute;