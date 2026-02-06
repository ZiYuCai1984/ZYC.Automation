namespace ZYC.Automation.Abstractions;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public sealed class UriQueryNameAttribute : Attribute
{
    public UriQueryNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}