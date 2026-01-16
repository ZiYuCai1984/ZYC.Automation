using ZYC.MdXaml.Plugins;

namespace ZYC.MdXaml.Extensions;

internal class ExtensionsPluginSetup : IPluginSetup
{
    public void Setup(MdXamlPlugins plugins)
    {
        plugins.Inline.Add(new EmojiInlineParser());
    }
}