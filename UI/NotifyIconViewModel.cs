using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using H.NotifyIcon;

namespace UI;

public partial class NotifyIconViewModel : ObservableRecipient, IRecipient<MainWindow.MainVisibilityChanged>
{
    public NotifyIconViewModel()
    {
        IsActive = true;
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ShowWindowCommand))]
    public bool canExecuteShowWindow = true;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(HideWindowCommand))]
    public bool canExecuteHideWindow;
    
    // <summary>
    /// Shows a window, if none is already open.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExecuteShowWindow))]
    public void ShowWindow()
    {
        Application.Current.MainWindow ??= new MainWindow();
        Application.Current.MainWindow.Show(true);
        CanExecuteShowWindow = false;
        CanExecuteHideWindow = true;
    }

    /// <summary>
    /// Hides the main window. This command is only enabled if a window is open.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExecuteHideWindow))]
    public void HideWindow()
    {
        Application.Current.MainWindow.Hide(true);
        CanExecuteShowWindow = true;
        CanExecuteHideWindow = false;
    }

    /// <summary>
    /// Shuts down the application.
    /// </summary>
    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current.Shutdown();
    }

    public void Receive(MainWindow.MainVisibilityChanged message)
    {
        if (message.Hide)
        {
            CanExecuteShowWindow = true;
            CanExecuteHideWindow = false;
        }
    }
}