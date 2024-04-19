using scoreboard2.Models.Common;

namespace scoreboard2.Models;

public class Period() : NamedValue("PERIOD")
{
    public override string ToString() => Value.ToString();
}