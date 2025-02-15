using Xunit;

namespace DefaultNamespace
{
    public class ClassStp
    {
        [Fact]
        public void TestTwoTwoLong()
        {
            {
                System.Threading.Thread.Sleep(541);
                Assert.Equal(1, 1);
            }
        }
    }
}
