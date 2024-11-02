using Avalonia.Controls;
using scoreboard2.Common;

namespace scoreboard2.Views.Baseball;

public class BaseballScorebugViewBase(ScorebugSize size) : UserControl, IScorebugView
{
    public ScorebugSize Size { get; } = size;
}