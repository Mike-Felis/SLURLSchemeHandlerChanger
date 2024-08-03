using System.Diagnostics;
using System.Security.Principal;
using SLSchemaUtil;

using ConsoleAppFramework;
using PPlus;

namespace SchemaChanger
{

    public class App
    {
        public static void Main(String[] args)
        {
            if (!IsAdministrator())
            {
//                Console.Error.WriteLine("管理者権限で実行してください。");
               Console.Error.WriteLine("Please Running Administrator");

                Environment.Exit(0);
            }

            var app = ConsoleApp.Create();
            app.Add<SchemaChanger.Commands>();
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
        private String protocol = "secondlife";
            /// <summary>
        /// Bind the running viewer to the schema.
        /// </summary>

        public void Apply()
        {
            var list = SLViewerSupplier.GetSLViewers();

            Func<Process, string> RunCommandFunc = (process) => $"\"{process.MainModule?.FileName}\" -url \"%1\"";
            new URLProtocolHandler<Process>(protocol, list.First(), RunCommandFunc);
        }

        /// <summary>
        /// Select from among the installed viewers and bind the Schema.
        /// </summary>
        public void list()
        {

            var list = SLViewerSupplier.GetInstalledSLViewersByRegistry();
//            var p = PromptPlus.Select<string>("どのビューワに紐づけますか？");
            var p = PromptPlus.Select<string>("To which viewer do you want to bind it?");

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
        }

    }
}
