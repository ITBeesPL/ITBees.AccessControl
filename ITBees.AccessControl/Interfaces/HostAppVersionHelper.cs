using System.Reflection;

namespace ITBees.AccessControl.Interfaces;

public static class HostAppVersionHelper
{
    public static string? GetHostAppInformationalVersion()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null)
            return null;

        var attr = entryAssembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>();

        return attr?.InformationalVersion; 
    }

    public static string? GetHostAppFileVersion()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null)
            return null;

        var attr = entryAssembly
            .GetCustomAttribute<AssemblyFileVersionAttribute>();

        return attr?.Version;
    }
}