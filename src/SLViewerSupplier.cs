namespace SLSchemaUtil;
using System.Diagnostics;
using System.Management;
using Microsoft.Win32;

public record AppInfo(string Name, string Path);

public interface IProcessSupplier
{
    public Process[] GetProcesses();
}

#pragma warning disable CA1416
public static class SLViewerSupplier
{
    public static List<AppInfo> GetInstalledSLViewersByRegistry()
    {
        string uninstallPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
        var uninstall = Registry.LocalMachine.OpenSubKey(uninstallPath, false);
        if (uninstall == null)
        {
            return [];
        }
        var list = uninstall.GetSubKeyNames().Select(e =>
        {
            var appKey = Registry.LocalMachine.OpenSubKey(uninstallPath + "\\" + e, false);
            return new AppInfo(Name: appKey?.GetValue("DisplayName")?.ToString() ?? ""
            , Path: appKey?.GetValue("DisplayIcon")?.ToString() ?? "");
        });
        return list.Where(e => e.Name.Contains("Alchemy") || e.Name.Contains("Firestorm") || e.Name.Contains("SecondLife")).ToList();
    }
    public static List<string> GetInstalledSLViewers()
    {
        using var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Product");
        using var colResult = searcher.Get();
        var lstSoft = new List<string>();
        if (colResult == null)
        {
            return [];
        }
        foreach (var objItem in colResult)
        {
            lstSoft.Add(objItem["Name"]?.ToString());
        }
        return lstSoft;
    }
    class ProcessSupplier : IProcessSupplier
    {

        Process[] IProcessSupplier.GetProcesses()
        {
            return Process.GetProcesses();
        }
    }

    public static List<Process> GetSLViewers()
    {
        var list = new List<Process>();
        //ローカルコンピュータ上で実行されているすべてのプロセスを取得
        Process[] ps = Process.GetProcesses();
        //配列から1つずつ取り出す
        foreach (Process p in ps)
        {
            if (p.ProcessName.Contains("Alchemy"))
            {
                list.Add(p);
            }
            if (p.ProcessName.Contains("Firestorm"))
            {
                list.Add(p);
            }
            if (p.ProcessName.Contains("SecondLifeViewer"))
            {
                list.Add(p);
            }
        }
        return list;
    }
}