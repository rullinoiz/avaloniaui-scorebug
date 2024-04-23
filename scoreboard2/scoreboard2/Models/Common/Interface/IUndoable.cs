namespace scoreboard2.Models.Common.Interface;

public interface IUndoable : ICommitable
{
    public int Undo();
}