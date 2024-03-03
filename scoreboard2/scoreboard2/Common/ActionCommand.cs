using System;
using System.Windows.Input;

namespace scoreboard2.Common;

public class ActionCommand<T>(Action<T> function) : ICommand
{
    public Action<T> Function { get; private set; } = function;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => Function.Invoke((T)parameter);

    public event EventHandler? CanExecuteChanged;
}