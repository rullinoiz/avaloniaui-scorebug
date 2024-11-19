using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using scoreboard2.ViewModels;
using scoreboard2.Views.Baseball;
using scoreboard2.Views.Basketball;
using scoreboard2.Views.Football;

#if !(BROWSER || IOS || ANDROID)
using scoreboard2.Common;
#endif

namespace scoreboard2.Views;

public partial class ScorebugConfigView : UserControl
{
    static ScorebugConfigView()
    {
        PopulateGameViews();
    }
    
    public ScorebugConfigView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        
        ViewModel.PropertyChanged += async (_, args) =>
        {
            if (args.PropertyName == nameof(ViewModel.SelectedGame))
            {
                await Dispatcher.UIThread.InvokeAsync(PopulateComboBox);
            }
        };
        _ = Dispatcher.UIThread.InvokeAsync(PopulateComboBox);
    }

    private MainViewModel ViewModel => (MainViewModel)DataContext!;
    private static readonly Type[] GameTypes = [typeof(BaseballScorebugViewBase), typeof(FootballScorebugViewBase), typeof(BasketballScorebugViewBase)]; 
    private static readonly List<Type>[] GameViews = new List<Type>[GameTypes.Length];

    public void SpawnClickButtonFunction()
    {
        var selectedGame = ViewModel.SelectedGame;
        var selectedView = GameViews[selectedGame][StyleDropDownComboBox.SelectedIndex];
        
#if !(BROWSER || IOS || ANDROID)
        var view = (IScorebugView)Activator.CreateInstance(selectedView)!;
        ViewModel.AddNewScorebugView(view);
#endif
    }
    
    /// <summary>
    /// This is a quick way to find all possible styles of scorebugs available for each game.
    ///
    /// Usually you'll only have to do this once
    /// </summary>
    private static void PopulateGameViews()
    {
        for (var i = 0; i < GameTypes.Length; i++)
        {
            var type = GameTypes[i];
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            var list = GameViews[i] ??= [];

            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsAssignableTo(type) && t != type)
                .OrderBy(t => t.Name);
            list.AddRange(classes);
        }
    }

    private Task PopulateComboBox()
    {
        Console.WriteLine("populate");
        var views = GameViews[ViewModel.SelectedGame];
        StyleDropDownComboBox.Items.Clear();

        foreach (var view in views)
        {
            StyleDropDownComboBox.Items.Add(new Label { Content = view.Name });
        }

        StyleDropDownComboBox.SelectedIndex = 0;
        return Task.CompletedTask;
    }
}