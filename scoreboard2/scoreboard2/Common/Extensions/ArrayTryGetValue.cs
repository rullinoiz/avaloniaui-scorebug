using System;

namespace scoreboard2.Common.Extensions;

public static class ArrayTryGetValue
{
    public static bool TryGetValue(this Array array, int index, out object? output)
    {
        try
        {
            output = array.GetValue(index);
            return true;
        }
        catch (IndexOutOfRangeException)
        {
            output = null;
            return false;
        }
    }
}