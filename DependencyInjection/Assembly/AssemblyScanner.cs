namespace DotnetMicroServiceFundamentals.DependencyInjection.Assembly;

public static class AssemblyScanner
{
    public static IEnumerable<System.Reflection.Assembly> GetReferencedAssemblies(System.Reflection.Assembly assembly)
    {
        var assemblies = new List<System.Reflection.Assembly> { assembly };
        var referencedAssemblies = assembly.GetReferencedAssemblies();

        assemblies.AddRange(referencedAssemblies.Select(referencedAssemblyName =>
            System.Reflection.Assembly.Load(referencedAssemblyName)));

        return assemblies;
    }
}