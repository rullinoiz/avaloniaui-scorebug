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

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
                break;
            // browser implementation (you can't make new windows on browsers apparently)
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