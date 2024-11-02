using Avalonia.Controls;
using scoreboard2.Common;

namespace scoreboard2.Windows;

public partial class ScoreboardWindow : Window
{
    // fixes avalonia warning because there is no visible parameterless constructor
    public ScoreboardWindow() {}
    
    public ScoreboardWindow(object? vm, IScorebugView view)
    {
        DataContext = vm;
        InitializeComponent();

        var control = (Control)view;

        StyleEnclosure.Child = control;
        Width = view.Size.Width;
        Height = view.Size.Height;
        Title = control.GetType().Name;
    }
}