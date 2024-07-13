using System.Diagnostics;
using Microsoft.Win32;




var list=SLViewerSupplier.GetSLViewers();
foreach(var s in list){
    Console.WriteLine(s.ProcessName);

}
Func<Process,String> RunCommandFunc = (process)=>$"\"{process.MainModule?.FileName}\" -url \"%1\"";
new URLProtocolHandler("secondlife",list.First(),RunCommandFunc);


