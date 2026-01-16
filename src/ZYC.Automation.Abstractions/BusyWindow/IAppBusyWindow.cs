namespace ZYC.Automation.Abstractions.BusyWindow;

public interface IAppBusyWindow
{
    IBusyWindowHandler Enqueue();
}