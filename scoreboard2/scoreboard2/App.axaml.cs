using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using scoreboard2.RemoteControl;
using scoreboard2.ViewModels;
using scoreboard2.Views;
using MainWindow = scoreboard2.Windows.MainWindow;

namespace scoreboard2;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var vm = new MainViewModel();
        ReplicatorService.Instance.RegisterProperties(vm);
        ReplicatorService.Instance.Setup(vm);
        // ReplicatorService.Instance.DebouncePropertyList.Add(vm.BaseGame.);
        
        //ReplicatorService.ConfigureSocket("ws://localhost:25565/replicator");

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                singleViewPlatform.MainView = new MainViewBrowser
                {
                    DataContext = vm
                };
                break;
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}