using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.CLI.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[Register]
internal class CLITabItem : TabItemInstanceBase<CLIView>
{
    private static readonly object Sync = new();
    private static readonly HashSet<int> UsedIndexes = new();

    public CLITabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
        Index = AllocateMinIndex();
    }

    public override bool Localization => false;

    public int Index { get; }

    public override string Title => $"{Constants.DefaultTitle} - {Index}";

    public override void Dispose()
    {
        ReleaseIndex(Index);

        base.Dispose();
    }

    private static int AllocateMinIndex()
    {
        lock (Sync)
        {
            var i = 1;
            while (UsedIndexes.Contains(i))
            {
                i++;
            }

            UsedIndexes.Add(i);
            return i;
        }
    }

    private static void ReleaseIndex(int index)
    {
        if (index <= 0)
        {
            return;
        }

        lock (Sync)
        {
            UsedIndexes.Remove(index);
        }
    }

    public class Constants
    {
        public static string Host => "cli";

        public static string DefaultTitle => "CLI";

        public static string Icon => "Console";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}