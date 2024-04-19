using System.Collections.Generic;
using scoreboard2.Models.Common;

namespace scoreboard2.Models;

public class Score(string name) : NamedValue(name), IUndoable
{
    private readonly Stack<int> _previousValues = new();

    public void Undo()
    {
        if (_previousValues.TryPop(out var v))
            Value = v;
    }

    public void Commit(int v)
    {
        _previousValues.Push(Value);
        Value = v;
    }
}