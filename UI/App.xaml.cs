using System.Configuration;
using System.Data;
using System.Windows;
using H.NotifyIcon;

namespace UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private TaskbarIcon? notifyIcon;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
        notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
        notifyIcon.ForceCreate();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        notifyIcon?.Dispose(); //the icon would clean up automatically, but this is cleaner
        base.OnExit(e);
    }
}