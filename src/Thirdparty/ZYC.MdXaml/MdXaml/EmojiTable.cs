using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ZYC.MdXaml;

public static class EmojiTable
{
    private static ConcurrentDictionary<string, string>? s_keywords;

    static EmojiTable()
    {
        s_keywords = LoadTxt();
    }


    /*
        Workaround for Visual Studio Xaml Designer.
        When you open MarkdownStyle from Xaml Designer,
        A null error occurs. Perhaps static constructor is not executed.
    */
    private static ConcurrentDictionary<string, string> LoadTxt()
    {
        var resourceName = "ZYC.MdXaml.MdXaml.EmojiTable.txt";
        var dic = new ConcurrentDictionary<string, string>();

        var asm = typeof(EmojiTable).Assembly;
#if DEBUG
        var names = asm.GetManifestResourceNames();
#endif

        using var stream = asm.GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            throw new InvalidOperationException($"fail to load '{resourceName}'");
        }

        using var reader = new StreamReader(stream, true);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var elms = line.Split('\t');
            dic[elms[1]] = elms[0];
        }

        return dic;
    }

    public static bool TryGet(
        string keyword,
#if !NETFRAMEWORK
        [MaybeNullWhen(false)]
#endif
        out string emoji)
    {
        if (s_keywords is null)
        {
            s_keywords = LoadTxt();
        }

        return s_keywords.TryGetValue(keyword, out emoji);
    }
}