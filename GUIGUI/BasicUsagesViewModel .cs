using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Data;
using Reactive.Bindings;
using SLSchemaUtil;
using System.Drawing;
using System.Collections.Immutable;
namespace GUIGUI;
public class BasicUsagesViewModel
{
    public ReactiveCollection<AppInfoWithIcon> Viewers { get; set; }

    public ReactiveProperty<bool> IsLunching { get; }
    public ReactiveProperty<bool> IsInstalled { get; }

    public ReactiveProperty<string> Guide { get; }

    public ReactiveProperty<AppInfoWithIcon> SelectedViewer { get; } = new ReactiveProperty<AppInfoWithIcon>();
    public ReactiveCommand BindCommand { get; }


    public BasicUsagesViewModel()
    {
        BindCommand = new ReactiveCommand();
        Guide = new ReactiveProperty<string>();
        Guide.Value = "どのビューワに紐づけますか？";
        Viewers = new ReactiveCollection<AppInfoWithIcon>();
        IsLunching = new ReactiveProperty<bool>(true);
        IsInstalled = new ReactiveProperty<bool>(false);
        var supplier = new SLViewerSupplier();
        IsLunching.Where(x => x == true).Subscribe(e =>
        {
            var count = Viewers.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                Viewers.RemoveAt(i);
            }

            var l = supplier.GetSLViewers().Select(e => new AppInfoWithIcon(e.MainModule.ModuleName, e.MainModule.FileName,null/*Icon.ExtractAssociatedIcon(e.MainModule.FileName)*/));
            foreach (var e2 in l)
            {
                Viewers.Add(e2);
            }
        });
        IsInstalled.Where(x => x == true).Subscribe(e =>
        {
            var count = Viewers.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                Viewers.RemoveAt(i);
            }
            var l = supplier.GetInstalledSLViewersByRegistry()/*.Select(e => e.Name)*/;
            foreach (var e2 in l)
            {
                Viewers.Add(new AppInfoWithIcon(e2.Name, e2.Path,null/*Icon.ExtractAssociatedIcon(e2.Path)*/));
            }
        });

        BindCommand.Subscribe((e) =>
            {
                static string RunCommandFunc(AppInfoWithIcon process) => $"{process.Path} -url \"%1\"";
                URLProtocolHandler<AppInfoWithIcon>.CreateInstance("secondlife", RunCommandFunc).Create(SelectedViewer.Value);
                Guide.Value = SelectedViewer.Value.Name + "に紐づけました";
            });

        SelectedViewer.Subscribe((e) =>
        {


        });
    }


}