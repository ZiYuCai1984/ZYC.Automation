namespace ZYC.Automation.Modules.MarkdownViewer.Abstractions;

public interface IMarkdownViewerTabItem
{
    Task UpdateMarkdownSourceAsync(MarkdownSource markdownSource);

    MarkdownSource? MarkdownSource { get; }
}