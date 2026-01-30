namespace ZYC.MdXaml.MdXaml.Extensions;

internal static class HtmlAllowList
{
    public static readonly HashSet<string> BlockTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "p", "div", "blockquote", "pre", "hr", "ul", "ol", "li",
        "table", "thead", "tbody", "tr", "th", "td",
        "details", "summary",
        "h1", "h2", "h3", "h4", "h5", "h6"
    };

    public static readonly HashSet<string> InlineTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "a", "img", "br", "strong", "b", "em", "i", "del", "s", "strike", "code", "kbd", "sub", "sup", "span", "input"
    };

    public static readonly HashSet<string> VoidTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "img", "br", "hr", "meta", "link", "input"
    };

    public static readonly HashSet<string> RejectTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "script", "style", "iframe", "object", "embed", "form", "button", "textarea", "select", "option",
        "video", "audio", "canvas", "svg", "math", "link", "meta", "base"
    };

    // Per-tag allowlisted attributes (lowercase)
    public static readonly Dictionary<string, HashSet<string>> AllowedAttributes = new(StringComparer.OrdinalIgnoreCase)
    {
        ["a"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "href", "title" },
        ["img"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "src", "alt", "title", "width", "height" },
        ["p"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["div"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["h1"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["h2"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["h3"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["h4"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["h5"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["h6"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["th"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align", "colspan", "rowspan" },
        ["td"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align", "colspan", "rowspan" },
        ["table"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "align" },
        ["details"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "open" },
        ["input"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "type", "checked", "disabled" }
        // span/code/kbd etc: keep none (we ignore style/class by design)
    };

    public static bool IsAllowedTag(string tagName)
    {
        return BlockTags.Contains(tagName) || InlineTags.Contains(tagName);
    }

    public static bool IsAllowedInlineTag(string tagName)
    {
        return InlineTags.Contains(tagName);
    }

    public static bool IsAllowedBlockTag(string tagName)
    {
        return BlockTags.Contains(tagName);
    }
}