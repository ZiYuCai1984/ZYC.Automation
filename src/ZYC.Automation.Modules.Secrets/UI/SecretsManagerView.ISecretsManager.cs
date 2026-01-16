using ZYC.Automation.Core;
using ZYC.Automation.Modules.Secrets.Abstractions;
using ZYC.Automation.Modules.Settings.Abstractions;

namespace ZYC.Automation.Modules.Secrets.UI;

internal sealed partial class SecretsManagerView : ISecretsManager
{
    public ISecrets[] Secrets { get; } = [];

    private TaskCompletionSource LoadedTaskCompletionSource { get; } = new();

    public ISecrets[] GetSecretsConfigs()
    {
        return Secrets.ToArray();
    }

    public void BringIntoView<T>()
    {
        BringIntoView(typeof(T));
    }

    public void BringIntoView(Type configType)
    {
        Task.Run(async () =>
        {
            await LoadedTaskCompletionSource.Task;
            await Dispatcher.InvokeAsync(() =>
            {
                FilterText = "";
                ItemsControl.BringIntoView((_, item) =>
                {
                    var group = (SettingGroup)item;
                    return group.Type == configType;
                });
            });
        });
    }

    public Uri GetPageUri()
    {
        return SecretsTabItem.Constants.Uri;
    }
}