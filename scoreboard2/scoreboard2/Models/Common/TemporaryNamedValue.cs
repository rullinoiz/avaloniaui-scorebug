namespace scoreboard2.Models.Common;

public class TemporaryNamedValue(string name, int clearValue = 0) : NamedValue(name)
{
    public void Clear() => Value = clearValue;
}