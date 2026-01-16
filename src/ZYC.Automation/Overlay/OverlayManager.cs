using System.Windows;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Overlay;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Overlay;

[RegisterSingleInstanceAs(typeof(IOverlayManager))]
internal class OverlayManager : IOverlayManager
{
    public OverlayManager(IMainWindow mainWindow)
    {
        MainWindow = mainWindow;
    }

    private IMainWindow MainWindow { get; }

    public IOverlay Show(object target, object? passThrough = null)
    {
        var targetElement = (UIElement)target;
        UIElement? passThroughElement = null;
        if (passThrough != null)
        {
            passThroughElement = (UIElement)passThrough;
        }

        var guideOverlay = new GuideOverlay((Window)MainWindow.GetMainWindow());
        guideOverlay.Show(targetElement, passThroughElement);

        return guideOverlay;
    }
}