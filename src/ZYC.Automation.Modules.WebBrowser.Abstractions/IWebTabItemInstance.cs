using ZYC.Automation.Abstractions.Tab;

namespace ZYC.Automation.Modules.WebBrowser.Abstractions;

public interface IWebTabItemInstance : ITabItemInstance
{
    void SetTitle(string title);

    void SetIcon(string icon);

    Task TabInternalNavigatingAsync(Uri newUri);
}