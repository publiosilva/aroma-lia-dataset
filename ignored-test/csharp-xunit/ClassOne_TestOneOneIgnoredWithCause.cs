using Xunit;

namespace TestAssetsXunit
{
    public class ClassOne
    {
        [Fact(Skip = "This is the ignore cause")]
        public void TestOneOneIgnoredWithCause()
        {
            {
                Assert.True(1 == 2, "This is ignored with cause, no failure");
            }
        }
    }
}
