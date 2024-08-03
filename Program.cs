using System.Diagnostics;
using System.Security.Principal;
using SLSchemaUtil;

using ConsoleAppFramework;
using PPlus;

var protocol = "secondlife";
if (!IsAdministrator()){
    Console.Error.WriteLine("管理者権限で実行してください。");
Environment.Exit(0);
    }

var app = ConsoleApp.Create();
app.Add("", () =>
{
    var list = SLViewerSupplier.GetSLViewers();

    Func<Process, string> RunCommandFunc = (process) => $"\"{process.MainModule?.FileName}\" -url \"%1\"";
    new URLProtocolHandler<Process>(protocol, list.First(), RunCommandFunc);

});
app.Add("list", () =>
{
    var protocol = "secondlife";

    var list = SLViewerSupplier.GetInstalledSLViewersByRegistry();
    var p = PromptPlus.Select<string>("どのビューワに紐づけますか？");
    list.ForEach(a => p.AddItem(a.Name));
    var result = p.Run();
    if (!result.IsAborted)
    {
        Console.WriteLine(result.Value);
        var appInfo = list.First(e => e.Name == result.Value);
        Console.WriteLine(appInfo);

        Func<AppInfo, String> RunCommandFunc = (process) => $"{process.Path} -url \"%1\"";
        new URLProtocolHandler<AppInfo>(protocol, appInfo, RunCommandFunc);
    }
});

app.Run(args);

bool IsAdministrator()
{
    var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
    return principal.IsInRole(WindowsBuiltInRole.Administrator);
}
