// https://stackoverflow.com/a/8735366

using System;

namespace scoreboard2.Common.Extensions;

public static class ObjectFirePublicEvent
{
    public static void FirePublicEvent(this object onMe, string invokeMe, params object[] eventParams)
    {
        var eventDelagate =
            onMe.GetType().GetField(invokeMe,
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public)!.GetValue(onMe) as MulticastDelegate;

        var delegates = eventDelagate!.GetInvocationList();

        foreach (var dlg in delegates)
        {
            dlg.Method.Invoke(dlg.Target, eventParams);
        }
    } 
}