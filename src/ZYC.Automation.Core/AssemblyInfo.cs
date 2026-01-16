// ReSharper disable all

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

[assembly: InternalsVisibleTo("ZYC.Automation")]
[assembly: InternalsVisibleTo("ZYC.Automation.CLI")]


#if DEBUG

#else
[assembly: XmlnsPrefix("https://github.com/ZiYuCai1984", "zyc")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Bindings")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Buttons")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Commands")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Converters")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Localizations")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.MarkupEx")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Menu")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.MenuItems")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Notification")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Banner")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Toast")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Page")]
[assembly: XmlnsDefinition("https://github.com/ZiYuCai1984", "ZYC.Automation.Core.Resources")]

#endif


namespace ZYC.Automation.Core;

public static class AssemblyInfo
{
    public static Assembly GetAssembly()
    {
        return typeof(AssemblyInfo).Assembly;
    }
}