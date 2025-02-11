using Xunit;

namespace TestAssetsXunit
{
    public class ClassOne
    {
        [Fact(Skip = "")]
        public void TestOneOneIgnored()
        {
            {
                Assert.True(1 == 2, "This is ignored, no failure");
            }
        }
    }
}
