using ZYC.MdXaml.Plugins;

namespace ZYC.MdXaml.MdXaml.Extensions;

internal class MarkdownHtmlContext : IMarkdownHtmlContext
{
    public MarkdownHtmlContext(IMarkdown markdown)
    {
        Markdown = markdown;
    }

    private IMarkdown Markdown { get; }

    public Uri? BaseUri => Markdown.BaseUri;

    public bool AllowDataImages => true;
}