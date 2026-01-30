namespace ZYC.MdXaml.MdXaml.Extensions;

internal static class HtmlUrlPolicy
{
    public static bool TrySanitizeUrl(
        string? raw,
        IMarkdownHtmlContext? ctx,
        bool allowDataImages,
        out Uri? uri)
    {
        uri = null;
        if (string.IsNullOrWhiteSpace(raw))
        {
            return false;
        }

        raw = raw.Trim();

        // Disallow javascript: etc.
        if (raw.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (raw.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (raw.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
        {
            return false; // usually unsafe in README renderers
        }

        if (allowDataImages && raw.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
        {
            // For data:image/... accept as a Uri; BitmapImage supports data URIs in newer WPF only sometimes.
            // If your WPF can't load it, your renderer can fallback to alt text.
            if (Uri.TryCreate(raw, UriKind.Absolute, out var dataUri))
            {
                uri = dataUri;
                return true;
            }

            return false;
        }

        // Absolute
        if (Uri.TryCreate(raw, UriKind.Absolute, out var abs))
        {
            if (abs.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
                abs.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase) ||
                abs.Scheme.Equals("mailto", StringComparison.OrdinalIgnoreCase))
            {
                uri = abs;
                return true;
            }

            return false;
        }

        // Relative allowed: resolve against BaseUri if provided, otherwise keep as relative
        if (Uri.TryCreate(raw, UriKind.Relative, out var rel))
        {
            var baseUri = ctx?.BaseUri;
            if (baseUri is not null && Uri.TryCreate(baseUri, rel, out var resolved))
            {
                uri = resolved;
                return true;
            }

            // Keep relative (still usable if host knows how to resolve)
            uri = rel;
            return true;
        }

        return false;
    }
}