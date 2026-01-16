using System.Windows;
using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Page;

[RegisterAs(typeof(InnerErrorView), typeof(IInnerErrorView))]
public partial class InnerErrorView : IInnerErrorView
{
    public InnerErrorView(Exception exception)
    {
        Exception = exception;
        InitializeComponent();
    }

    public Exception Exception { get; }

    private bool FirstRending { get; set; } = true;

    private void OnInnerErrorViewLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;
        TextEditor.Text = Exception.ToString();
    }
}