namespace ZYC.Automation.Modules.TaskManager.Abstractions.Event;

/// <summary>
///     Raised when task progress changes.
/// </summary>
public sealed class ManagedTaskProgressChangedEvnet
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ManagedTaskProgressChangedEvnet" /> class.
    /// </summary>
    /// <param name="snapshot">The task snapshot.</param>
    public ManagedTaskProgressChangedEvnet(TaskRecord snapshot)
    {
        Snapshot = snapshot;
    }

    /// <summary>
    ///     Gets the task snapshot.
    /// </summary>
    public TaskRecord Snapshot { get; }
}