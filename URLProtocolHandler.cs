namespace SLSchemaUtil;

using Microsoft.Win32;

public class URLProtocolHandler<T>
{
    public URLProtocolHandler(String protocol, T process, Func<T, String> runCommand)
    {
        var key = Key(protocol);
        key.SetValue(string.Empty, "URL: Second Life");
        key.SetValue("URL Protocol", string.Empty);
        var subKey = createOpenCommandKey(key);
        subKey.SetValue(string.Empty, runCommand(process), RegistryValueKind.ExpandString);
        key.Close();
    }
    RegistryKey createOpenCommandKey(RegistryKey key)
    {
        return key.CreateSubKey(@"shell\open\command");
    }
    RegistryKey Key(String protocol)
    {
        return Registry.ClassesRoot.CreateSubKey(protocol);

    }
}
