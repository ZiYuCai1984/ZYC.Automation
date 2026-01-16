using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Windows;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.State;
using ZYC.Automation.CLI;
using ZYC.Automation.Core;
using ZYC.Automation.Core.Localizations;
using ZYC.Automation.Infrastructure;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.Automation.Modules.Settings.Abstractions.Event;
using ZYC.Automation.WebView2;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Settings;
using AssemblyInfo = ZYC.Automation.Core.AssemblyInfo;

namespace ZYC.Automation;

#pragma warning disable CA1416

internal partial class Program
{
    private static IDisposable? AppConfigChangedEvent { get; set; }

    private static void StartApp()
    {
        var mainAppFolder = AppContext.GetMainAppDirectory();

        var appState = SettingsTools.GetFromFolder<AppState>(
            mainAppFolder);

        if (appState.StartupTarget == StartupTarget.Main)
        {
            if (AppContext.IsSelfAlternate())
            {
                RestartToMainApp();
            }
            else
            {
                return;
            }
        }
        else if (appState.StartupTarget == StartupTarget.Alternate)
        {
            if (!AppContext.IsSelfAlternate())
            {
                RestartToAlternateApp();
            }
            else
            {
                return;
            }
        }

        throw new InvalidOperationException($"Unknown {nameof(StartupTarget)}:{appState.StartupTarget}");
    }


    private static void RestartToMainApp()
    {
        var fileName = AppContext.GetProcessFileName();

        var mainAppFolder = AppContext.GetMainAppDirectory();
        var mainAppPath = Path.Combine(mainAppFolder, fileName);

        Process.Start(
            new ProcessStartInfo(mainAppPath,
                AppContext.GetArgumentString())
            {
                WorkingDirectory = mainAppFolder
            });

        AppContext.FocusExitProcess();
    }

    private static void RestartToAlternateApp()
    {
        var fileName = AppContext.GetProcessFileName();
        var folder = AppContext.GetAlternateAppDirectory();

        var alternateAppPath = Path.Combine(folder,
            fileName);

        Process.Start(new ProcessStartInfo(
            alternateAppPath,
            AppContext.GetArgumentString())
        {
            WorkingDirectory = folder
        });

        AppContext.FocusExitProcess();
    }

    [STAThread]
    private static void Main()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

#if DEBUG

#else
        EnsureSingleInstance();
#endif

        InitJsonToolsSettings();

        StartApp();

        var builder = new ContainerBuilder();

        builder.RegisterGeneric(typeof(NullLogger<>))
            .As(typeof(IAppLogger<>));

        var appContextDirectory = AppContext.GetMainAppDirectory();


        ModuleTools.RegisterAllFromAssembly(appContextDirectory,
            builder,
            typeof(Program).Assembly);
        ModuleTools.RegisterAllFromAssembly(appContextDirectory,
            builder,
            AssemblyInfo.GetAssembly());
        ModuleTools.RegisterAllFromAssembly(appContextDirectory,
            builder,
            typeof(WebViewHostBase).Assembly);


        //!WARNING Obfuscation will cause errors in loading the wpf resource dictionary, so it is placed in a separate assembly
        ModuleTools.RegisterAllFromAssembly(appContextDirectory,
            builder,
            MetroWindow.AssemblyInfo.GetAssembly());

        ModuleConfig? moduleConfig = null;
        PendingFileOperationsState? pendingDeleteState = null;

        ModuleTools.RegisterAllFromAssembly(appContextDirectory,
            builder,
            typeof(ProductInfo).Assembly, obj =>
            {
                if (obj is ModuleConfig initModuleConfig)
                {
                    moduleConfig = initModuleConfig;
                }

                if (obj is PendingFileOperationsState initPendingFileOperationsState)
                {
                    pendingDeleteState = initPendingFileOperationsState;
                }
            });

        Debug.Assert(moduleConfig != null);
        Debug.Assert(pendingDeleteState != null);

        var startupLogger = StartupLogger.CreateInstance();
        var modules = ModuleTools.RegisterModules(
            appContextDirectory,
            builder,
            moduleConfig,
            pendingDeleteState,
            startupLogger);

        var container = builder.Build();
        foreach (var module in modules)
        {
            if (!module.IsEnabled)
            {
                continue;
            }

            module.LoadAsync(container).Wait();
        }

        foreach (var module in modules)
        {
            if (!module.IsEnabled)
            {
                continue;
            }

            module.AfterLoadedAsync(container).Wait();
        }

        L.SetLifetimeScope(container);

        var mainWindowView = container.Resolve<MainWindowView>();
        var mainWindow = container.Resolve<IMainWindow>();
        mainWindow.InitContent(mainWindowView);

        var app = container.Resolve<AppContext>();
        RegisterAppConfigChangedCallback(container);

        var window = (Window)mainWindow;
        app.Run(window);
    }

    private static void RegisterAppConfigChangedCallback(IContainer container)
    {
        //TODO Design failure, just adding this is not enough !!
        if (!container.TryResolve<ISettingsManager>(out _))
        {
            MessageBoxTools.Warning("Missing Settings module,some features don't work properly !!");
            return;
        }

        var processFileName = container.Resolve<IAppContext>()
            .GetProcessFileName();
        var logger = container.Resolve<IAppLogger<AppContext>>();

        var eventAggregator = container.Resolve<IEventAggregator>();
        AppConfigChangedEvent = eventAggregator.Subscribe<SettingChangedEvent<AppConfig>>(e =>
        {
            var oldValue = e.OldValue;
            var newValue = e.NewValue;

            ApplyDesktopShortcut(processFileName, oldValue, newValue);
            ApplyStartAtBoot(oldValue, newValue, logger);
            ApplyShowInTaskbar(container, oldValue, newValue);
        });
    }

    private static void ApplyShowInTaskbar(IContainer container, AppConfig oldValue, AppConfig newValue)
    {
        if (oldValue.ShowInTaskbar == newValue.ShowInTaskbar)
        {
            return;
        }

        var mainWindow = container.Resolve<IMainWindow>();
        mainWindow.SetShowInTaskbar(newValue.ShowInTaskbar);
    }

    private static void ApplyStartAtBoot(AppConfig oldValue, AppConfig newValue,
        IAppLogger<AppContext> logger)
    {
        if (oldValue.StartAtBoot == newValue.StartAtBoot)
        {
            return;
        }

        try
        {
            if (newValue.StartAtBoot)
            {
                ShortcutTools.AddToStartupFolder();
            }
            else
            {
                ShortcutTools.RemoveFromStartupFolder();
            }
        }
        catch (Exception e)
        {
            logger.Error(e);
        }
    }


    private static void ApplyDesktopShortcut(string processFileName, AppConfig oldValue, AppConfig newValue)
    {
        if (oldValue.DesktopShortcut == newValue.DesktopShortcut)
        {
            return;
        }

        if (newValue.DesktopShortcut)
        {
            ShortcutTools.CreateFromCurrentProcess();
        }
        else
        {
            var fileNameWithoutExe = IOTools.GetFileName(
                processFileName,
                false);

            ShortcutTools.Delete($"{fileNameWithoutExe}.lnk");
        }
    }


    private static void InitJsonToolsSettings()
    {
        JsonTools.SetDefaultJsonSerializerOptions(new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            Converters = { new JsonStringEnumConverter() }
        });
    }
}