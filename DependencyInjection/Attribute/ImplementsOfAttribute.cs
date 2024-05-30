namespace ServiceBase.DependencyInjection.Attribute;

[AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
public sealed class ImplementsOfAttribute : System.Attribute
{
    public Type ImplementationType { get; }

    public ImplementsOfAttribute(Type implementationType)
    {
        ImplementationType = implementationType;
    }
}