using Avalonia.Controls;

namespace scoreboard2.Windows;

public partial class ScoreboardWindow : Window
{
    // fixes avalonia warning because there is no visible parameterless constructor
    public ScoreboardWindow() {}
    
    public ScoreboardWindow(object? vm)
    {
        DataContext = vm;
        InitializeComponent();
    }
}