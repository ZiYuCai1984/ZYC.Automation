using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Abstractions.Autofac;

namespace ZYC.Automation.Modules.ModuleManager;

internal class NuGetModule : INuGetModule, INotifyPropertyChanged
{
    private bool _installed;
    private string _patchNote = "";

    public NuGetModule(string packageId, string version, string description, bool installed)
    {
        PackageId = packageId;
        Version = version;
        Description = description;
        Installed = installed;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Icon => "PackageVariantClosed";

    public string Description { get; }

    public string ModulePath => string.Empty;

    public string ModuleAssemblyName => PackageId;

    public string[] ReferenceAssemblyNames { get; } = [];

    public IModuleInfo[] DependencyOn { get; } = [];

    public IModuleInfo[] DependencyBy { get; } = [];

    public string PackageId { get; }

    public string Version { get; }

    public bool Installed
    {
        get => _installed;
        internal set
        {
            _installed = value;
            OnPropertyChanged();
        }
    }

    public string PatchNote
    {
        get => _patchNote;
        internal set
        {
            _patchNote = value;
            OnPropertyChanged();
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}