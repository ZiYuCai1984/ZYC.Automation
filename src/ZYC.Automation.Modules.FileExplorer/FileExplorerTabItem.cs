using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.FileExplorer.Abstractions;
using ZYC.Automation.Modules.FileExplorer.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.FileExplorer;

[Register]
[ConstantsSource(typeof(FileExplorerModuleConstants))]
public class FileExplorerTabItem : TabItemInstanceBase, IFileExplorerTabItemInstance, INotifyPropertyChanged
{
    public FileExplorerTabItem(
        ILifetimeScope lifetimeScope,
        ITabManager tabManager,
        Uri uri) : base(lifetimeScope)
    {
        TabManager = tabManager;
        Uri = uri;
    }

    private ITabManager TabManager { get; }

    public override bool Localization => false;

    public override string Title => Uri.UnescapeDataString(Uri.Segments.Last());

    public new string Icon { get; set; } = FileExplorerModuleConstants.Icon;

    public override object View => _view ??= LifetimeScope.Resolve<FileExplorerView>(
        new TypedParameter(typeof(Uri), Uri),
        new TypedParameter(typeof(IFileExplorerTabItemInstance), this));

    public async Task TabInternalNavigatingAsync(Uri newUri)
    {
        var oldUri = Uri;
        Uri = newUri;

        if (UriTools.Equals(oldUri, newUri))
        {
            return;
        }

        OnPropertyChanged(nameof(Title));
        await TabManager.TabInternalNavigatingAsync(oldUri, newUri);
    }

    public void UpdateIcon(string base64Icon)
    {
        Icon = base64Icon;
        OnPropertyChanged(nameof(Icon));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}