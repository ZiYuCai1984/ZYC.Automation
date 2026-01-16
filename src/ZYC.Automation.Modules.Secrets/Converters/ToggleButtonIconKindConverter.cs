using System.Globalization;
using ZYC.Automation.Core.Converters;

namespace ZYC.Automation.Modules.Secrets.Converters;

internal class ToggleButtonIconKindConverter : ConverterBase
{
    public string TrueValue { get; set; } = "LockOpenOutline";
    public string FalseValue { get; set; } = "LockOutline";

    public bool Reverse { get; set; }

    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = (bool)value! ^ Reverse;

        if (result)
        {
            return TrueValue;
        }

        return FalseValue;
    }
}