using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace ZYC.MdXaml.MdXaml.Extensions;

internal static class HtmlDom
{
    public static IElement ParseAsWrapper(string html)
    {
        var parser = new HtmlParser();
        // Wrap fragment so we can always return a single root element.
        var id = "__wpf_md_html_root__";
        var doc = parser.ParseDocument($"<div id='{id}'>{html}</div>");
        return doc.GetElementById(id)!;
    }
}