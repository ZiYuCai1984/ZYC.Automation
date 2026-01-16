using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Autofac;
using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Core;

public class Component : UserControl, IDisposable
{
    public static readonly DependencyProperty LifetimeScopeProperty = DependencyProperty.Register(
        nameof(LifetimeScope),
        typeof(ILifetimeScope),
        typeof(Component), new PropertyMetadata(null, OnLifetimeScopeChanged));

    public Component()
    {
        SetBinding(LifetimeScopeProperty, new Binding(nameof(LifetimeScope))
        {
            RelativeSource = new RelativeSource(
                RelativeSourceMode.FindAncestor,
                typeof(IMainWindow), 1)
        });

        Loaded += OnComponentLoaded;
    }

    public Type Type { get; set; } = null!;

    public ILifetimeScope LifetimeScope
    {
        get => (ILifetimeScope)GetValue(LifetimeScopeProperty);
        set => SetValue(LifetimeScopeProperty, value);
    }

    private bool FirstRending { get; set; } = true;

    public void Dispose()
    {
        Loaded -= OnComponentLoaded;
    }

    private static void OnLifetimeScopeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
    }

    private void OnComponentLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;

        Debug.Assert(Type != null);
        Debug.Assert(LifetimeScope != null);

        Content = LifetimeScope.Resolve(Type);
    }
}