using Xunit;

namespace DefaultNamespace
{
    public class LayoutTypedTests
    {
        [Fact]
        public void LayoutFixedNullIntValueTest()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.Null(result);
                Assert.Null(result5);
                Assert.Equal("", layout.Render(LogEventInfo.CreateNullEvent()));
                Assert.True(layout.IsFixed);
                Assert.Null(layout.FixedValue);
                Assert.Equal("null", layout.ToString());
                Assert.Equal(nullValue, layout);
                Assert.NotEqual(0, layout);
            }
        }
    }
}
