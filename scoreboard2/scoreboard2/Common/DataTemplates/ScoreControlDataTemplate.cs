using Avalonia.Controls;
using Avalonia.Controls.Templates;
using scoreboard2.Controls;
using scoreboard2.Models;

namespace scoreboard2.Common.DataTemplates;

public class ScoreControlDataTemplate : IDataTemplate
{
    public Control Build(object param)
    {
        var s = (Score)param;
        return new QuickNumberControl() { Title = s.Name, CurrentValue = s.Value, CommitClickFunction = s.Commit };
    }

    public bool Match(object param)
    {
        return param is Score;
    }
}