using ZYC.Automation.Core.Converters;
using ZYC.Automation.Modules.TaskManager.Abstractions;

namespace ZYC.Automation.Modules.TaskManager.Converters;

internal class ProgressMultiValueConverter : MultiValueConverterBase<double?, ManagedTaskState, double>
{
    protected override double Convert(double? progress, ManagedTaskState state)
    {
        if (state == ManagedTaskState.Completed)
        {
            return 1.0;
        }

        if (progress == null)
        {
            return 0.0;
        }

        return progress.Value;
    }
}