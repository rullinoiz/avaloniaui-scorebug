namespace scoreboard2.Models;

public interface IUndoable
{
    public void Undo();

    public void Commit(int v);
}