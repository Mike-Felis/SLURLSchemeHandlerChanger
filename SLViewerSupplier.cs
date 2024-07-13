using System.Diagnostics;

public static class SLViewerSupplier
{
    public static List<Process> GetSLViewers()
    {
        var list = new List<Process>();
        //ローカルコンピュータ上で実行されているすべてのプロセスを取得
        Process[] ps = Process.GetProcesses();
        //配列から1つずつ取り出す
        foreach (Process p in ps)
        {
            try
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

                //Console.WriteLine( $"プロセス: {p.ProcessName} {p.Id} ;{p.MainModule.FileName}" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                //		Console.WriteLine($"エラー: {ex.Message}");
            }
        }
        return list;

    }
}