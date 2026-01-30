using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.WebBrowser.Abstractions;
using ZYC.Automation.Modules.WebBrowser.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.WebBrowser;


[Register]
[ConstantsSource(typeof(WebBrowserModuleConstants))]
internal class WebBrowserTabItem : TabItemInstanceBase, IWebTabItemInstance, INotifyPropertyChanged
{
    public WebBrowserTabItem(
        ILifetimeScope lifetimeScope,
        Uri uri,
        ITabManager tabManager) : base(lifetimeScope)
    {
        Uri = uri;
        TabManager = tabManager;
        Title = uri.Host;
    }

    private ITabManager TabManager { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public new string Icon { get; set; } = WebBrowserModuleConstants.MenuIcon;

    public override bool Localization => false;

    public new string Title { get; private set; }

    public override object View => _view ??= LifetimeScope.Resolve<WebBrowserView>(
        new TypedParameter(typeof(Uri), Uri),
        new TypedParameter(typeof(IWebTabItemInstance), this));


    public void SetTitle(string title)
    {
        Title = title;
        OnPropertyChanged(nameof(Title));
    }

    public void SetIcon(string icon)
    {
        Icon = icon;
        OnPropertyChanged(nameof(Icon));
    }

    public async Task TabInternalNavigatingAsync(Uri newUri)
    {
        var oldUri = Uri;
        Uri = newUri;

        if (UriTools.Equals(oldUri, newUri))
        {
            return;
        }

        await TabManager.TabInternalNavigatingAsync(oldUri, newUri);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}