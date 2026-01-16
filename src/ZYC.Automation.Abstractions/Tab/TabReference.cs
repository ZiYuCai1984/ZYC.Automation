namespace ZYC.Automation.Abstractions.Tab;

/// <summary>
///     !WARNING Design flaw: GUID and Uri should not be modified arbitrarily by external parties (compromise for
///     serialization convenience)
/// </summary>
public class TabReference : IEquatable<TabReference>
{
    // ReSharper disable once UnusedMember.Global
    public TabReference()
    {
        //For serialization
    }

    public TabReference(Uri uri)
    {
        Uri = uri;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public Uri Uri { get; set; } = null!;

    public DateTime CreateTime { get; set; } = DateTime.Now;

    public bool Equals(TabReference? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TabReference);
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return Id.GetHashCode();
    }

    public static bool operator ==(TabReference? left, TabReference? right)
    {
        return EqualityComparer<TabReference>.Default.Equals(left, right);
    }

    public static bool operator !=(TabReference? left, TabReference? right)
    {
        return !(left == right);
    }
}

public static class TabReferenceEx
{
    public static TabReference[] RemoveReference(
        this TabReference[] references,
        TabReference item)
    {
        var list = new List<TabReference>(references);
        list.Remove(item);
        return list.ToArray();
    }

    public static TabReference[] AddReference(
        this TabReference[] references,
        TabReference item)
    {
        var list = new List<TabReference>(references) { item };
        return list.ToArray();
    }
}