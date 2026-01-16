using System.Diagnostics;
using System.Windows;
using System.Windows.Forms.Integration;
using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.FileExplorer.Abstractions;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.FileExplorer.UI;

[Register]
internal partial class FileExplorerView : IDisposable
{
    public FileExplorerView(
        IAppLogger<FileExplorerView> logger,
        Uri uri,
        IFileExplorerTabItemInstance fileExplorerTabItemInstance,
        IAppContext appContext)
    {
        Logger = logger;
        FileExplorerTabItemInstance = fileExplorerTabItemInstance;
        AppContext = appContext;

        InitializeComponent();

        var explorerBrowser = new ExplorerBrowser();

        explorerBrowser.NavigationOptions.PaneVisibility.Navigation = PaneVisibilityState.Hide;
        explorerBrowser.NavigationOptions.PaneVisibility.Commands = PaneVisibilityState.Hide;
        explorerBrowser.NavigationComplete += OnExplorerBrowserNavigationComplete;

        //!WARNING If an exception is thrown here, explorerBrowser will be released automatically, so there's no need to worry about memory leaks.
        explorerBrowser.Navigate(ShellObject.FromParsingName(
            uri.ToString()));

        ExplorerBrowser = explorerBrowser;
        var windowsFormsHost = new WindowsFormsHost();
        windowsFormsHost.Child = ExplorerBrowser;

        Content = windowsFormsHost;

        //explorerBrowser.ForeColor = Color.FromArgb(45, 45, 200);
    }

    private ExplorerBrowser ExplorerBrowser { get; }

    private IAppLogger<FileExplorerView> Logger { get; }
    private IFileExplorerTabItemInstance FileExplorerTabItemInstance { get; }
    private IAppContext AppContext { get; }

    private bool FirstRending { get; set; } = true;

    private bool Disposing { get; set; }

    public void Dispose()
    {
        if (Disposing)
        {
            Debugger.Break();
            return;
        }

        Disposing = true;

        Loaded -= OnFileExplorerViewLoaded;
        ExplorerBrowser.NavigationComplete -= OnExplorerBrowserNavigationComplete;

        ExplorerBrowser.Dispose();

        var content = Content;
        Content = null;
        content?.TryDispose();
    }

    private void OnFileExplorerViewLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;
    }

    private async void OnExplorerBrowserNavigationComplete(object? sender, NavigationCompleteEventArgs e)
    {
        var path = e.NewLocation.ParsingName;

        await FileExplorerTabItemInstance.TabInternalNavigatingAsync(
            new Uri($"file:///{path}"));

        _ = Task.Run(() =>
        {
            try
            {
                var base64Icon = ShellIconBase64.TryGetFolderIconPngBase64(path);
                if (string.IsNullOrWhiteSpace(base64Icon))
                {
                    return;
                }

                AppContext.InvokeOnUIThread(() =>
                {
                    FileExplorerTabItemInstance.UpdateIcon(base64Icon);
                });
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        });
    }
}