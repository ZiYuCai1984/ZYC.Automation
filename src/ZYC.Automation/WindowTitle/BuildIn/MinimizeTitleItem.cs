using System.Windows.Input;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Core.WindowTitle;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WindowTitle.BuildIn;

[RegisterSingleInstance]
internal class MinimizeTitleItem : WindowTitleItem
{
    public MinimizeTitleItem(MinimizeCommand minimizeCommand) : base("WindowMinimize", null!)
    {
        MinimizeCommand = minimizeCommand;
    }

    private MinimizeCommand MinimizeCommand { get; }

    public override ICommand Command => MinimizeCommand;
}