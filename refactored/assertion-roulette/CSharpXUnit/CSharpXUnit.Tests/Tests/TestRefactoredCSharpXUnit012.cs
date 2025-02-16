using Xunit;

public class LayoutTypedTests
{
    [Fact]
    public void LayoutNotEqualsNullIntValueFixedTest()
    {
        // Arrange
        int? nullInt = null;
        var layout1 = new Layout<int>("2");
        Layout<int>? layout2 = nullInt;
        // Act + Assert
        Assert.False(layout1 == nullInt, "Explanation message");
        Assert.False(layout1.Equals(nullInt), "Explanation message");
        Assert.False(layout1.Equals((object?)nullInt), "Explanation message");
        Assert.NotEqual(layout1, layout2, "Explanation message");
        Assert.NotEqual(layout1.GetHashCode(), layout2?.GetHashCode(), "Explanation message");
    }
}
