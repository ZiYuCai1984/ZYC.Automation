using System.Windows.Input;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Core.WindowTitle;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WindowTitle.BuildIn;

[RegisterSingleInstance]
public class ExitProcessTitleItem : WindowTitleItem
{
    public ExitProcessTitleItem(
        ExitProcessCommand exitProcessCommand) : base("Close", null!)
    {
        ExitProcessCommand = exitProcessCommand;
    }

    private ExitProcessCommand ExitProcessCommand { get; }

    public override ICommand Command => ExitProcessCommand;
}