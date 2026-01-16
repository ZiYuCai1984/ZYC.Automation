using System.Diagnostics;
using System.Windows.Input;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.SideBar;
using ZYC.Automation.Abstractions.StatusBar;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Core.Commands;

namespace ZYC.Automation.Core;

public static class LifetimeScopeTools
{
    extension(ILifetimeScope lifetimeScope)
    {
        public void PublishEvent<T>(T @event) where T : notnull
        {
            var eventAggregator = lifetimeScope.Resolve<IEventAggregator>();
            eventAggregator.Publish(@event);
        }

        public IDisposable SubscribeEvent<TEvent>(Action<TEvent> handler,
            bool onUiThread = false) where TEvent : notnull
        {
            var eventAggregator = lifetimeScope.Resolve<IEventAggregator>();
            return eventAggregator.Subscribe(handler, onUiThread);
        }

        public void RegisterDefaultSideBarItem<T>() where T : ISideBarItem
        {
            var provider = lifetimeScope.Resolve<IDefaultSideBarItemsProvider>();
            provider.RegisterItem<T>();
        }

        public void RegisterDefaultStatucBarItem<T>() where T : IStatusBarItem
        {
            var provider = lifetimeScope.Resolve<IDefaultStatusBarItemsProvider>();
            provider.RegisterItem<T>();
        }

        public void RegisterRootMainMenuItem<T>() where T : IMainMenuItem
        {
            var mainMenuManager = lifetimeScope.Resolve<IMainMenuManager>();
            mainMenuManager.RegisterItem<T>();
        }

        public void RegisterTabItemFactory<T>() where T : ITabItemFactory
        {
            var tabItemFactoryManager = lifetimeScope.Resolve<ITabItemFactoryManager>();
            tabItemFactoryManager.RegisterFactory<T>();
        }

        public void RegisterSimpleTabItemFactory(SimpleTabItemFactoryInfo info)
        {
            var simpleTabItemFactoryManager = lifetimeScope.Resolve<ISimpleTabItemFactoryManager>();
            simpleTabItemFactoryManager.Register(info);
        }

        public void RegisterWorkspaceDragDropProvider<T>()
            where T : IWorkspaceDragDropProvider
        {
            var workspaceDragDropManager = lifetimeScope.Resolve<IWorkspaceDragDropManager>();
            workspaceDragDropManager.Register<T>();
        }

        public ICommand CreateNavigateCommand(Uri uri)
        {
            var tabManager = lifetimeScope.Resolve<ITabManager>();
            return new RelayCommand(_ => true,
                _ => tabManager.NavigateAsync(uri));
        }

        public void CheckIsRoot()
        {
            var tag = lifetimeScope.Tag.ToString();
            if (tag == "root")
            {
                return;
            }

            Debugger.Break();
        }

        public void CheckIsNotRoot()
        {
            var tag = lifetimeScope.Tag.ToString();
            if (tag != "root")
            {
                return;
            }

            Debugger.Break();
        }
    }
}