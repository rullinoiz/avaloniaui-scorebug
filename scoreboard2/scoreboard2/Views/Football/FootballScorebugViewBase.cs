using Avalonia.Controls;
using scoreboard2.Common;

namespace scoreboard2.Views.Football;

public partial class FootballScorebugViewBase(ScorebugSize size) : UserControl, IScorebugView
{
    public ScorebugSize Size { get; } = size;
}