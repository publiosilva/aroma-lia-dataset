using Xunit;

namespace DefaultNamespace
{
    public class LayoutTypedTests
    {
        [Fact]
        public void LayoutNotEqualsNullIntValueFixedTest()
        {
            {
                // Arrange
                int? nullInt = null;
                Layout<int?> layout1 = "2";
                Layout<int?> layout2 = nullInt;
                // Act + Assert
                Assert.False(layout1 == nullInt);
                Assert.False(layout1.Equals(nullInt));
                Assert.False(layout1.Equals((object)nullInt));
                Assert.NotEqual(layout1, layout2);
                Assert.NotEqual(layout1.GetHashCode(), layout2.GetHashCode());
            }
        }
    }
}
