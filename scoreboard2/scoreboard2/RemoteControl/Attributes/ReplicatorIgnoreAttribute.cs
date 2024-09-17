using System;

namespace scoreboard2.RemoteControl.Attributes;

/// <summary>
/// Tells the ReplicatorService to ignore a property when it changes.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ReplicatorIgnoreAttribute : Attribute;