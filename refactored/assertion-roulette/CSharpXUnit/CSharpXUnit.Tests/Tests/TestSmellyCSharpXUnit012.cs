using Xunit;

namespace DefaultNamespace
{
    public class LayoutTypedTests
    {
        [Fact]
        public void LayoutNotEqualsNullIntValueFixedTest1()
        {
            {
                // Arrange
                int? nullInt = null;
                Layout<int?> layout1 = "2";
                Layout<int?> layout2 = nullInt;
                // Act + Assert
                Assert.False(layout1 == nullInt);
            }
        }

        [Fact]
        public void LayoutNotEqualsNullIntValueFixedTest2()
        {
            {
                // Arrange
                int? nullInt = null;
                Layout<int?> layout1 = "2";
                Layout<int?> layout2 = nullInt;
                // Act + Assert
                Assert.False(layout1.Equals(nullInt));
            }
        }

        [Fact]
        public void LayoutNotEqualsNullIntValueFixedTest3()
        {
            {
                // Arrange
                int? nullInt = null;
                Layout<int?> layout1 = "2";
                Layout<int?> layout2 = nullInt;
                // Act + Assert
                Assert.False(layout1.Equals((object)nullInt));
            }
        }

        [Fact]
        public void LayoutNotEqualsNullIntValueFixedTest4()
        {
            {
                // Arrange
                int? nullInt = null;
                Layout<int?> layout1 = "2";
                Layout<int?> layout2 = nullInt;
                // Act + Assert
                Assert.NotEqual(layout1, layout2);
            }
        }

        [Fact]
        public void LayoutNotEqualsNullIntValueFixedTest5()
        {
            {
                // Arrange
                int? nullInt = null;
                Layout<int?> layout1 = "2";
                Layout<int?> layout2 = nullInt;
                // Act + Assert
                Assert.NotEqual(layout1.GetHashCode(), layout2.GetHashCode());
            }
        }
    }
}
