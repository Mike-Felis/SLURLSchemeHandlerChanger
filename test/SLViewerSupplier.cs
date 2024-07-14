//using test;
using SLSchemaUtil;

namespace Test
{
    public class CalcTest
    {
        [Fact]
        public void test()
        {
            var list =  SLViewerSupplier.GetInstalledSLViewersByRegistry();
            Assert.NotNull(list);
            Assert.Equal(list.Count,4);
            Assert.Equal(list[0].Name,"Alchemy Beta 7.1.9.2492");
            Assert.Equal(list[0].Path,"C:\\Program Files\\AlchemyBeta\\AlchemyBeta.exe");

        }
    }
}