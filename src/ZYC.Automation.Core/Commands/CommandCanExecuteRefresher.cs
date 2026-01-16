using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[Register]
public sealed class CommandCanExecuteRefresher : IDisposable
{
    private readonly HashSet<CommandBase> _commands = new();
    private readonly object _gate = new();
    private bool _disposed;

    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            foreach (var cmd in _commands)
            {
                cmd.Executed -= OnCommandExecuted;
            }

            _commands.Clear();
        }
    }

    /// <summary>
    ///     Register a command into the group: when it finishes, refreshes CanExecute on other commands.
    /// </summary>
    public CommandCanExecuteRefresher Register(CommandBase command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        lock (_gate)
        {
            ThrowIfDisposed();
            if (!_commands.Add(command))
            {
                return this;
            }

            command.Executed += OnCommandExecuted;
            return this;
        }
    }

    /// <summary>
    ///     Register multiple commands.
    /// </summary>
    public CommandCanExecuteRefresher RegisterRange(IEnumerable<CommandBase> commands)
    {
        if (commands is null)
        {
            throw new ArgumentNullException(nameof(commands));
        }

        foreach (var c in commands)
        {
            Register(c);
        }

        return this;
    }

    /// <summary>
    ///     Remove a command.
    /// </summary>
    public bool Unregister(CommandBase command)
    {
        if (command is null)
        {
            return false;
        }

        lock (_gate)
        {
            if (_disposed)
            {
                return false;
            }

            if (!_commands.Remove(command))
            {
                return false;
            }

            command.Executed -= OnCommandExecuted;
            return true;
        }
    }

    /// <summary>
    ///     Manual trigger: raise CanExecuteChanged for all commands in the group (call when needed).
    /// </summary>
    public void RefreshAll()
    {
        CommandBase[] snapshot;
        lock (_gate)
        {
            ThrowIfDisposed();
            snapshot = _commands.ToArray();
        }

        foreach (var cmd in snapshot)
        {
            cmd.RaiseCanExecuteChanged();
        }
    }

    private void OnCommandExecuted(object? sender, EventArgs e)
    {
        // sender is the command that just finished executing.
        var executed = sender as CommandBase;

        CommandBase[] snapshot;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            snapshot = _commands.ToArray();
        }

        foreach (var cmd in snapshot)
        {
            // Only refresh other commands.
            if (executed != null && ReferenceEquals(cmd, executed))
            {
                continue;
            }

            cmd.RaiseCanExecuteChanged();
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(CommandCanExecuteRefresher));
        }
    }
}
