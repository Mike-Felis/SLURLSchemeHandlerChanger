using System.Diagnostics;
using Microsoft.Win32;

public class URLProtocolHandler
{
    public URLProtocolHandler(String protocol, Process process, Func<Process, String> runCommand)
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
