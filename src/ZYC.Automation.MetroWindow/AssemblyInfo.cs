using System.Reflection;

namespace ZYC.Automation.MetroWindow;

public static class AssemblyInfo
{
    public static Assembly GetAssembly()
    {
        return typeof(AssemblyInfo).Assembly;
    }
}