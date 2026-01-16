namespace ZYC.Automation.Abstractions.Notification;

public interface INotification
{
    event EventHandler Closed;

    object GetVisibility();
}