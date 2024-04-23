using System;
using Avalonia.Controls;
using scoreboard2.Models;
using scoreboard2.ViewModels.Common;
using scoreboard2.Windows;

namespace scoreboard2.ViewModels;

public class MainViewModel : ViewModelBase
{
    public BaseballGame Game { get; } = new();

    public MainViewModel()
    {
        Console.WriteLine("instantiating main view model");
        
        Window scoreboardWindow = new ScoreboardWindow(this);
        scoreboardWindow.Show();
    }
}