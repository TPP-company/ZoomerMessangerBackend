namespace ZM.Infrastructure.RoutePrefix;

[AttributeUsage(AttributeTargets.Class)]
public class RoutePrefixAttribute(string prefix) : Attribute
{
	public string Prefix { get; } = prefix;
}