using System.Diagnostics;
using System.Security.Principal;
using SLSchemaUtil;

using ConsoleAppFramework;
using PPlus;

namespace SchemaChanger
{

#pragma warning disable CA1416
    public class App
    {
        public static void Main(string[] args)
        {
            if (!IsAdministrator())
            {
                Console.Error.WriteLine("Please Running Administrator");
                Environment.Exit(0);
            }
            var app = ConsoleApp.Create();
            app.Add<Commands>();
            app.Run(args);

        }
        static bool IsAdministrator()
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

    }

    public class Commands
    {
        private readonly string protocol = "secondlife";
        /// <summary>
        /// Bind the running viewer to the schema.
        /// </summary>

        public void Apply()
        {
            var list = new SLViewerSupplier().GetSLViewers();
            static string RunCommandFunc(Process process) => $"\"{process.MainModule?.FileName}\" -url \"%1\"";
            URLProtocolHandler<Process>.CreateInstance(protocol, RunCommandFunc).Create(list.First());
        }

        /// <summary>
        /// Select from among the installed viewers and bind the Schema.
        /// </summary>
        public void List()
        {
            var list = new SLViewerSupplier().GetInstalledSLViewersByRegistry();
            var p = PromptPlus.Select<string>("To which viewer do you want to bind it?");
            list.ForEach(a => p.AddItem(a.Name));
            var result = p.Run();
            if (!result.IsAborted)
            {
                Console.WriteLine(result.Value);
                var appInfo = list.First(e => e.Name == result.Value);
                Console.WriteLine(appInfo);

                static string RunCommandFunc(AppInfo process) => $"{process.Path} -url \"%1\"";
                URLProtocolHandler<AppInfo>.CreateInstance(protocol, RunCommandFunc).Create(appInfo);
            }
        }
    }
}
