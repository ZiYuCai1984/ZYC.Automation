namespace ZYC.Automation.Abstractions.Notification.Toast;

public class ToastMessage
{
    public ToastMessage(string text, string icon = "InformationOutline", bool localization = true)
    {
        Text = text;
        Icon = icon;
        Localization = localization;
    }

    public string Icon { get; }

    public string Text { get; }

    public bool Localization { get; }
}