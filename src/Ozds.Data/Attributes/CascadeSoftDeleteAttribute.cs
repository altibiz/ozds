namespace Ozds.Data.Attributes;

// TODO: implement in interceptor

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class CascadeSoftDeleteAttribute : Attribute
{
}
