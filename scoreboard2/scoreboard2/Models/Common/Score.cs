using System.Collections.Generic;
using scoreboard2.Models.Common.Interface;

namespace scoreboard2.Models.Common;

public class Score(string name) : NamedValue(name), IUndoable
{
    private readonly Stack<int> _previousValues = new();

    public int Undo()
    {
        return !_previousValues.TryPop(out var v) ? 0 : v;
    }

    public void Commit(int v)
    {
        _previousValues.Push(Value);
        Value = v;
    }
}