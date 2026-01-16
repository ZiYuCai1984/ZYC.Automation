using System.Windows.Input;
using ZYC.Automation.Commands;
using ZYC.Automation.Core.WindowTitle;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WindowTitle.BuildIn;

[RegisterSingleInstance]
internal class FreezeWindowTitleItem : WindowTitleItem
{
    public FreezeWindowTitleItem(
        FreezeWindowCommand freezeWindowCommand) : base("Snowflake", null!)
    {
        FreezeWindowCommand = freezeWindowCommand;
    }

    private FreezeWindowCommand FreezeWindowCommand { get; }

    public override ICommand Command => FreezeWindowCommand;
}