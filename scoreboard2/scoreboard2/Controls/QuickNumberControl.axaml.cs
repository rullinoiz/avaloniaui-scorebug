using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace scoreboard2.Controls;

public class QuickNumberControl : TemplatedControl
{
    #region boilerplate
    public static readonly StyledProperty<string> TitleProperty = 
        AvaloniaProperty.Register<QuickNumberControl, string>("Title", "HOME");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<int> CurrentValueProperty = 
        AvaloniaProperty.Register<QuickNumberControl, int>("CurrentValue");

    public int CurrentValue
    {
        get => GetValue(CurrentValueProperty);
        set => SetValue(CurrentValueProperty, value);
    }

    public static readonly StyledProperty<Action<int>> CommitClickFunctionProperty 
        = AvaloniaProperty.Register<QuickNumberControl, Action<int>>("CommitClickFunction");

    public Action<int> CommitClickFunction
    {
        get => GetValue(CommitClickFunctionProperty);
        init => SetValue(CommitClickFunctionProperty, value);
    }
    #endregion

    public void OnCommitButtonClicked(int value)
    {
        _previousValues.Push(CurrentValue);
        Console.WriteLine(value + " " + CurrentValue);
        CommitClickFunction.Invoke(value);
    }

    private readonly Stack<int> _previousValues = new();
    private TextBox? _editValue;

    public void ButtonClickFunction(int value)
    {
        _editValue!.Text = (int.Parse(_editValue.Text!) + value).ToString();
    }
    
    public void RevertClickFunction()
    {
        if (!_previousValues.TryPop(out var t)) return;
        Console.WriteLine("undo " + t);
        _editValue!.Text = t.ToString();
    }

    public void ResetClickFunction() => _editValue!.Text = CurrentValue.ToString();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _editValue = e.NameScope.Find<TextBox>(name:"EditValueBox")!;
        Console.WriteLine($"Loaded {Title}!");
    }
}