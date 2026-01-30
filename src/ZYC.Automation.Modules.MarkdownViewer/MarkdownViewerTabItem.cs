using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.MarkdownViewer.Abstractions;
using ZYC.Automation.Modules.MarkdownViewer.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.MarkdownViewer;

[RegisterAs(typeof(MarkdownViewerTabItem), typeof(IMarkdownViewerTabItem))]
internal class MarkdownViewerTabItem : TabItemInstanceBase, IMarkdownViewerTabItem, INotifyPropertyChanged
{
    public MarkdownViewerTabItem(
        ITabManager tabManager,
        ILifetimeScope lifetimeScope,
        MarkdownSource? markdownSource) : base(lifetimeScope)
    {
        TabManager = tabManager;

        if (markdownSource != null)
        {
            MarkdownSource = markdownSource;
            Uri = MarkdownRoute.BuildWithDocument(markdownSource.SourceUri);
        }
    }

    public override string Title
    {
        get
        {
            if (MarkdownSource == null)
            {
                return Constants.Title;
            }

            return Uri.UnescapeDataString(MarkdownSource.SourceUri.Segments.Last());
        }
    }

    public override object View => LifetimeScope.Resolve<MarkdownViewerView>(
        new TypedParameter(typeof(IMarkdownViewerTabItem), this));

    private ITabManager TabManager { get; }

    public MarkdownSource? MarkdownSource { get; private set; }

    public async Task UpdateMarkdownSourceAsync(MarkdownSource markdownSource)
    {
        var oldUri = Uri;
        Uri = MarkdownRoute.BuildWithDocument(markdownSource.SourceUri);

        if (UriTools.Equals(oldUri, Uri))
        {
            return;
        }

        MarkdownSource = markdownSource;
        await TabManager.TabInternalNavigatingAsync(oldUri, Uri);

        OnPropertyChanged(nameof(Title));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public class Constants
    {
        public static string Host => "md";

        public static string Title => "MarkdownViewer";

        public static string Icon => "📄";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}