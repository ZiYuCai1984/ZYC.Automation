namespace ZYC.Automation.Modules.MarkdownViewer.Abstractions;

//TODO-zyc Move to Abstractions !!
public class MarkdownSource
{
    public MarkdownSource(Uri sourceUri, Uri baseUri)
    {
        SourceUri = sourceUri;
        BaseUri = baseUri;
    }

    public Uri SourceUri { get; }

    public Uri BaseUri { get; }
}