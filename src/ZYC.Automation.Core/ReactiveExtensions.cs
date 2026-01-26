using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Core;

// ReSharper disable SuspiciousTypeConversion.Global
public static class ReactiveExtensions
{
    public static IObservable<T> ObserveAnyChange<T>(this T persistedData) where T : IPersistedData
    {
        if (persistedData is not INotifyPropertyChanged t)
        {
            throw new InvalidOperationException();
        }

        return Observable
            .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => t.PropertyChanged += h,
                h => t.PropertyChanged -= h)
            .Select(_ => persistedData);
    }

    public static IObservable<Unit> ObserveProperty<T>(this T persistedData, string propertyName)
    {
        if (persistedData is not INotifyPropertyChanged t)
        {
            throw new InvalidOperationException();
        }

        return Observable
            .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => t.PropertyChanged += h,
                h => t.PropertyChanged -= h)
            .Where(e => string.Equals(e.EventArgs.PropertyName, propertyName, StringComparison.Ordinal))
            .Select(_ => Unit.Default);
    }
}