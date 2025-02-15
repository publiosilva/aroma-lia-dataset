using Xunit;

namespace DefaultNamespace
{
    public class ClassStp
    {
        [Fact(Skip = "")]
        public void TestOneOneIgnoredWithCause()
        {
            {
                Assert.True(1 == 2, "This is ignored with cause, no failure");
            }
        }
    }
}
