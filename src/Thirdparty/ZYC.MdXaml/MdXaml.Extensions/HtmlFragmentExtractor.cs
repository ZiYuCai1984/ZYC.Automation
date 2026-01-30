namespace ZYC.MdXaml.MdXaml.Extensions;

internal static class HtmlFragmentExtractor
{
    // Extracts a single HTML element/comment/PI/CDATA starting at '<'
    // Returns end index (exclusive).
    public static bool TryExtractElementOrComment(string text, int startLt, out int endExclusive)
    {
        endExclusive = startLt;
        if (startLt < 0 || startLt >= text.Length || text[startLt] != '<')
        {
            return false;
        }

        // Comment
        if (StartsWith(text, startLt, "<!--"))
        {
            var end = text.IndexOf("-->", startLt + 4, StringComparison.Ordinal);
            if (end < 0)
            {
                return false;
            }

            endExclusive = end + 3;
            return true;
        }

        // CDATA
        if (StartsWith(text, startLt, "<![CDATA["))
        {
            var end = text.IndexOf("]]>", startLt + 9, StringComparison.Ordinal);
            if (end < 0)
            {
                return false;
            }

            endExclusive = end + 3;
            return true;
        }

        // Processing instruction <? ... ?>
        if (StartsWith(text, startLt, "<?"))
        {
            var end = text.IndexOf("?>", startLt + 2, StringComparison.Ordinal);
            if (end < 0)
            {
                return false;
            }

            endExclusive = end + 2;
            return true;
        }

        // Declaration <!DOCTYPE ...> or <! ... >
        if (StartsWith(text, startLt, "<!"))
        {
            var end = ScanToTagEnd(text, startLt + 2);
            if (end < 0)
            {
                return false;
            }

            endExclusive = end;
            return true;
        }

        // Normal tag: must be opening, not closing
        if (startLt + 1 < text.Length && text[startLt + 1] == '/')
        {
            return false;
        }

        if (!TryReadTagInfo(text, startLt, out var tagName, out var startTagEnd, out var selfClosing))
        {
            return false;
        }

        if (selfClosing || HtmlAllowList.VoidTags.Contains(tagName))
        {
            endExclusive = startTagEnd;
            return true;
        }

        // Find matching end tag accounting for nesting of same tag name
        if (!FindMatchingEndTag(text, tagName, startTagEnd, out var closeEnd))
        {
            return false;
        }

        endExclusive = closeEnd;
        return true;
    }

    private static bool FindMatchingEndTag(string text, string tagName, int scanStart, out int endExclusive)
    {
        endExclusive = -1;
        var depth = 1;
        var i = scanStart;

        while (i < text.Length)
        {
            var lt = text.IndexOf('<', i);
            if (lt < 0)
            {
                return false;
            }

            // Skip comments / cdata / pi quickly
            if (StartsWith(text, lt, "<!--"))
            {
                var cend = text.IndexOf("-->", lt + 4, StringComparison.Ordinal);
                if (cend < 0)
                {
                    return false;
                }

                i = cend + 3;
                continue;
            }

            if (StartsWith(text, lt, "<![CDATA["))
            {
                var cend = text.IndexOf("]]>", lt + 9, StringComparison.Ordinal);
                if (cend < 0)
                {
                    return false;
                }

                i = cend + 3;
                continue;
            }

            if (StartsWith(text, lt, "<?"))
            {
                var pend = text.IndexOf("?>", lt + 2, StringComparison.Ordinal);
                if (pend < 0)
                {
                    return false;
                }

                i = pend + 2;
                continue;
            }

            if (StartsWith(text, lt, "<!"))
            {
                var dend = ScanToTagEnd(text, lt + 2);
                if (dend < 0)
                {
                    return false;
                }

                i = dend;
                continue;
            }

            // Parse a tag
            if (!TryReadTagInfo(text, lt, out var name, out var tagEnd, out var selfClosing))
            {
                i = lt + 1;
                continue;
            }

            var isClose = lt + 1 < text.Length && text[lt + 1] == '/';
            var isVoid = HtmlAllowList.VoidTags.Contains(name);

            if (name.Equals(tagName, StringComparison.OrdinalIgnoreCase))
            {
                if (isClose)
                {
                    depth--;
                    if (depth == 0)
                    {
                        endExclusive = tagEnd;
                        return true;
                    }
                }
                else if (!selfClosing && !isVoid)
                {
                    depth++;
                }
            }

            i = tagEnd;
        }

        return false;
    }

    private static bool TryReadTagInfo(string text, int ltIndex, out string tagName, out int tagEndExclusive,
        out bool selfClosing)
    {
        tagName = "";
        tagEndExclusive = -1;
        selfClosing = false;

        if (ltIndex < 0 || ltIndex >= text.Length || text[ltIndex] != '<')
        {
            return false;
        }

        var i = ltIndex + 1;
        var isClose = false;
        if (i < text.Length && text[i] == '/')
        {
            isClose = true;
            i++;
        }

        // read tag name
        var nameStart = i;
        while (i < text.Length && IsNameChar(text[i]))
        {
            i++;
        }

        if (i == nameStart)
        {
            return false;
        }

        tagName = text.Substring(nameStart, i - nameStart);

        // scan to end '>'
        var end = ScanToTagEnd(text, i);
        if (end < 0)
        {
            return false;
        }

        // self-closing if ends with "/>"
        selfClosing = !isClose && end - 2 >= 0 && text[end - 2] == '/';

        tagEndExclusive = end;
        return true;
    }

    private static int ScanToTagEnd(string text, int start)
    {
        bool inSingle = false, inDouble = false;

        for (var i = start; i < text.Length; i++)
        {
            var c = text[i];
            if (c == '\'' && !inDouble)
            {
                inSingle = !inSingle;
            }
            else if (c == '"' && !inSingle)
            {
                inDouble = !inDouble;
            }
            else if (c == '>' && !inSingle && !inDouble)
            {
                return i + 1;
            }
        }

        return -1;
    }

    private static bool StartsWith(string s, int idx, string prefix)
    {
        if (idx < 0 || idx + prefix.Length > s.Length)
        {
            return false;
        }

        return s.AsSpan(idx, prefix.Length).SequenceEqual(prefix.AsSpan());
    }

    private static bool IsNameChar(char c)
    {
        return char.IsLetterOrDigit(c) || c == '-' || c == ':' || c == '_';
    }
}