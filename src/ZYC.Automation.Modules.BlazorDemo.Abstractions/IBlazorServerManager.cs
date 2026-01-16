namespace ZYC.Automation.Modules.BlazorDemo.Abstractions;

public interface IBlazorServerManager
{
    Task<IBlazorServer> StartBlazorServerAsync<TRootComponent>(string wwwrootFolder);
}