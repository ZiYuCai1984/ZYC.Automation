using ZYC.CoreToolkit;

namespace ZYC.Automation.Build.NuGet.SharedSources
{
    public static class Entry
    {
        public static async Task Main()
        {
            await CommandTools.ExecuteProgramAsync("ZYC.Automation.exe", "");
        }
    }
}
