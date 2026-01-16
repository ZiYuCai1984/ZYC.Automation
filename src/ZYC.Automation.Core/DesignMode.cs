using System.ComponentModel;
using System.Windows;

namespace ZYC.Automation.Core;

public static class DesignMode
{
    public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
}