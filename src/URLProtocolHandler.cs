using Microsoft.Win32;

namespace SLSchemaUtil;
#pragma warning disable S101
#pragma warning disable CA1416

public class URLProtocolHandler<T>
{
    readonly string protocol;
    readonly Func<T, string> runCommand;
    private URLProtocolHandler(string protocol, Func<T, string> runCommand)
    {
        this.protocol = protocol;
        this.runCommand = runCommand;
    }
    public static URLProtocolHandler<T> CreateInstance(string protocol, Func<T, string> runCommand)
    {
        return new URLProtocolHandler<T>(protocol, runCommand);

    }

    public void Create(T process)
    {
        using var key = URLProtocolHandler<T>.Key(protocol);
        key.SetValue(string.Empty, "URL: Second Life");
        key.SetValue("URL Protocol", string.Empty);
        using var subKey = URLProtocolHandler<T>.CreateOpenCommandKey(key);
        subKey.SetValue(string.Empty, runCommand(process), RegistryValueKind.ExpandString);

    }

    static RegistryKey CreateOpenCommandKey(RegistryKey key)
    {
        return key.CreateSubKey(@"shell\open\command");
    }

    static RegistryKey Key(string protocol)
    {
        return Registry.ClassesRoot.CreateSubKey(protocol);

    }
}
