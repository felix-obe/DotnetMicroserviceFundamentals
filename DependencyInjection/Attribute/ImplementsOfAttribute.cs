namespace DotnetMicroServiceFundamentals.DependencyInjection.Attribute;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public sealed class ImplementsOfAttribute : System.Attribute
{
    public ImplementsOfAttribute(Type implementationType)
    {
        ImplementationType = implementationType;
    }

    public Type ImplementationType { get; }
}