namespace ZYC.Automation.Abstractions.Tab;

public sealed class MutableTabReference : TabReference
{
    public MutableTabReference(Uri uri) : base(uri)
    {
    }

    public new Uri Uri
    {
        get => base.Uri;
        set => base.Uri = value;
    }
}