using System;
using Avalonia.Controls;
using Avalonia.Dialogs.Internal;
using scoreboard2.ViewModels;

namespace scoreboard2.Windows;

public partial class ScoreboardWindow : Window
{
    public ScoreboardWindow(object? vm)
    {
        DataContext = vm;
        Console.WriteLine(vm is null);
        InitializeComponent();
    }
}