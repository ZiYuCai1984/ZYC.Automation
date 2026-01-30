namespace ZYC.Automation.Abstractions.Tab;

public sealed class TabReference : IEquatable<TabReference>
{
    public TabReference()
    {
    }

    public TabReference(Uri uri)
    {
        Uri = uri;
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public Uri Uri { get; set; } = null!;

    public DateTimeOffset CreateTime { get; init; } = DateTimeOffset.UtcNow;

    public bool Equals(TabReference? other)
    {
        return other is not null && (ReferenceEquals(this, other) || Id == other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TabReference);
    }

    public override int GetHashCode()
    {
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

    public override string ToString()
    {
        return $"{Id} {Uri}";
    }
}

public static class TabReferenceEx
{
    extension(TabReference[] references)
    {
        public TabReference[] RemoveReference(TabReference item)
        {
            var list = new List<TabReference>(references);
            list.Remove(item);
            return list.ToArray();
        }

        public TabReference[] AddReference(TabReference item)
        {
            var list = new List<TabReference>(references) { item };
            return list.ToArray();
        }
    }
}