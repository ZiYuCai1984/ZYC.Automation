using System.Windows.Data;
using ZYC.Automation.Core.Converters;

namespace ZYC.Automation.Core.Bindings;

public class LBinding : Binding
{
    public LBinding()
    {
        Initialize();
    }

    public LBinding(string path) : base(path)
    {
        Initialize();
    }

    private void Initialize()
    {
        Converter = new TranslatorWrapperConverter(Converter);
    }
}