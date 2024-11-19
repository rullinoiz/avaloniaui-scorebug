using Avalonia.Controls;
using scoreboard2.Common;

namespace scoreboard2.Views.Basketball;

public class BasketballScorebugViewBase(ScorebugSize size) : UserControl, IScorebugView
{
    public ScorebugSize Size { get; } = size;
}