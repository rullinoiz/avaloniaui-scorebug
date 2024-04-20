namespace scoreboard2.Models.Common;

public interface IUndoable
{
    public int Undo();

    public void Commit(int v);
}