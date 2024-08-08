namespace test;

using Xunit;
using SLSchemaUtil;
using Moq;
using System.Diagnostics;

public class UnitTest1
{
    [Fact]
    public void GetSLViewers()
    {
        var mock = new Mock<Process>();
    //     mock.Setup(library => library.GetProcesses())
    //   .Returns(true);
        Assert.NotEmpty(
        SLViewerSupplier.GetSLViewers())
        ;
                Assert.Equal(1,
        SLViewerSupplier.GetSLViewers().Count)
        ;

    }
    
    [Fact]
    public void GetInstalledSLViewersByRegistry(){
        Assert.NotEmpty(
        SLViewerSupplier.GetInstalledSLViewersByRegistry())
        ;
                Assert.Equal(4,
        SLViewerSupplier.GetInstalledSLViewersByRegistry().Count)
        ;

    }
}
