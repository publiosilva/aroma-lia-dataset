using Xunit;

namespace DefaultNamespace
{
    public class LogExtensionsTests
    {
        [Fact]
        public void Can_Write_Verbose_Message_With_Default_Verbosity()
        {
            {
                // Given
                var log = new TestLog();
                // When
                log.Verbose("Hello World");
                // Then
                Assert.Equal(Verbosity.Verbose, log.Verbosity);
                Assert.Equal(LogLevel.Verbose, log.Level);
                Assert.Equal("Hello World", log.Message);
            }
        }
    }
}
