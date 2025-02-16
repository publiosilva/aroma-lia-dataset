using Xunit;

public class LayoutTypedTests
{
    [Fact]
    public void LayoutFixedNullIntValueTest()
    {
        // Arrange
        int? nullValue = null;
        var layout = new Layout<int?>(nullValue);
        // Act
        var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
        var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
        // Assert
        Assert.Null(result, "Explanation message");
        Assert.Null(result5, "Explanation message");
        Assert.Equal("", layout.Render(LogEventInfo.CreateNullEvent()), "Explanation message");
        Assert.True(layout.IsFixed(), "Explanation message");
        Assert.Null(layout.GetFixedValue(), "Explanation message");
        Assert.Equal("null", layout.ToString(), "Explanation message");
        Assert.Equal(nullValue, layout, "Explanation message");
        Assert.NotEqual(0, layout, "Explanation message");
    }
}
