using ZYC.MdXaml.MdXaml.Extensions;
using ZYC.MdXaml.Plugins;

namespace ZYC.MdXaml.Extensions;

internal class ExtensionsPluginSetup : IPluginSetup
{
    public void Setup(MdXamlPlugins plugins)
    {
        plugins.TopBlock.Add(new HtmlBlockParser());

        plugins.Inline.Add(new EmojiInlineParser());
        plugins.Inline.Add(new HtmlInlineParser());
    }
}