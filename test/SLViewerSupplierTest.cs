namespace test;

using Xunit;
using SLSchemaUtil;
using Moq;
using System.Diagnostics;

public class SLViewerSupplierTest
{
    [Fact]
    public void GetSLViewers()
    {
        var list = new SLViewerSupplier().GetSLViewers();
        Assert.NotEmpty(list);
        Assert.Equal(1, list.Count);
    }

    [Fact]
    public void GetInstalledSLViewersByRegistry()
    {
        var list = new SLViewerSupplier().GetInstalledSLViewersByRegistry();
        Assert.NotEmpty(list);
        Assert.Equal(4, list.Count);

    }
}
