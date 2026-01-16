using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.State;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class RestartAsAdminCommand : RestartCommand
{
    public RestartAsAdminCommand(
        IAppContext appContext,
        IEventAggregator eventAggregator,
        IAppLogger<RestartCommand> logger,
        DesktopWindowState desktopWindowState) : base(
        appContext,
        eventAggregator,
        logger,
        desktopWindowState)
    {
    }

    protected override bool IsAdministrator => true;
}