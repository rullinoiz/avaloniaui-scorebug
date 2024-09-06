using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace scoreboard2.Windows;

public partial class ScorebugConfig : Window
{
    // fixes avalonia warning because there is no visible parameterless constructor
    public ScorebugConfig() {}
    
    public ScorebugConfig(object? vm)
    {
        DataContext = vm;
        InitializeComponent();
    }
}