namespace ServiceBase.DependencyInjection.Assembly;
using System.Collections.Generic;
using System.Reflection;

public static class AssemblyScanner
{
    public static IEnumerable<Assembly> GetReferencedAssemblies(Assembly assembly)
    {
        var assemblies = new List<Assembly> { assembly };
        var referencedAssemblies = assembly.GetReferencedAssemblies();

        assemblies.AddRange(referencedAssemblies.Select(referencedAssemblyName => Assembly.Load(referencedAssemblyName)));

        return assemblies;
    }
}