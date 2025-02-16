using Xunit;

namespace DefaultNamespace
{
    public class LayoutTypedTests
    {
        [Fact]
        public void LayoutFixedNullIntValueTest1()
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
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest2()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.Null(result5);
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest3()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.Equal("", layout.Render(LogEventInfo.CreateNullEvent()));
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest4()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.True(layout.IsFixed);
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest5()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.Null(layout.FixedValue);
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest6()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.Equal("null", layout.ToString());
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest7()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.Equal(nullValue, layout);
            }
        }

        [Fact]
        public void LayoutFixedNullIntValueTest8()
        {
            {
                // Arrange
                var nullValue = (int? )null;
                Layout<int?> layout = new Layout<int?>(nullValue);
                // Act
                var result = layout.RenderValue(LogEventInfo.CreateNullEvent());
                var result5 = layout.RenderValue(LogEventInfo.CreateNullEvent(), 5);
                // Assert
                Assert.NotEqual(0, layout);
            }
        }
    }
}
