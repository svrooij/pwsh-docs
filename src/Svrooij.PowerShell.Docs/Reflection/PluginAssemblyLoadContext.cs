using System.Reflection;
using System.Runtime.Loader;

namespace Svrooij.PowerShell.Docs.Reflection;

internal class PluginAssemblyLoadContext : AssemblyLoadContext
{
    private readonly string _pluginDirectory;
    private readonly AssemblyDependencyResolver _resolver;

    public PluginAssemblyLoadContext(string pluginPath) : base(isCollectible: false)
    {
        _pluginDirectory = Path.GetDirectoryName(pluginPath)!;
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        // First try the dependency resolver (uses the .deps.json if available)
        string? resolvedPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (resolvedPath != null)
        {
            return LoadFromAssemblyPath(resolvedPath);
        }

        // Fall back to probing the plugin directory directly
        string probePath = Path.Combine(_pluginDirectory, assemblyName.Name + ".dll");
        if (File.Exists(probePath))
        {
            return LoadFromAssemblyPath(probePath);
        }

        // Let the default context handle it (shared framework assemblies, etc.)
        return null;
    }
}
