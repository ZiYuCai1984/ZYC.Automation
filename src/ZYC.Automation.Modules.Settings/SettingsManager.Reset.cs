using System.Reflection;
using Autofac;
using ZYC.Automation.Modules.Secrets.Abstractions;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.Settings;

public partial class SettingsManager
{
    public async Task ResetStatesAsync()
    {
        await Task.CompletedTask;

        var states = LifetimeScope.Resolve<IState[]>();
        foreach (var state in states)
        {
            ResetObject(state);
        }
    }

    public async Task ResetConfigsAsync()
    {
        await Task.CompletedTask;

        var config = Configs.ToArray();

        foreach (var c in config)
        {
            ResetObject(c);
        }
    }

    public async Task ResetSecretsAsync()
    {
        await Task.CompletedTask;

        var secretsManager = LifetimeScope.Resolve<ISecretsManager>();
        var secrets = secretsManager.GetSecretsConfigs();
        foreach (var s in secrets)
        {
            ResetObject(s);
        }
    }

    /// <summary>
    ///     Reset property value to new object instance's property value.
    /// </summary>
    private static void ResetObject(object obj)
    {
        var type = obj.GetType();
        var defaultInstance = Activator.CreateInstance(type);

        if (defaultInstance == null)
        {
            return;
        }

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite);

        foreach (var prop in props)
        {
            var defaultValue = prop.GetValue(defaultInstance);
            prop.SetValue(obj, defaultValue);
        }
    }
}