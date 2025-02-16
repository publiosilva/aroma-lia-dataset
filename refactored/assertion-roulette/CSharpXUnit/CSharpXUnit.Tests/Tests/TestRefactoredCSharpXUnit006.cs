using Xunit;

namespace DefaultNamespace
{
    public class LogExtensionsTests
    {
        [Fact]
        public void Can_Write_Verbose_Message_With_Default_Verbosity1()
        {
            {
                // Given
                var log = new TestLog();
                // When
                log.Verbose("Hello World");
                // Then
                Assert.Equal(Verbosity.Verbose, log.Verbosity);
            }
        }

        [Fact]
        public void Can_Write_Verbose_Message_With_Default_Verbosity2()
        {
            {
                // Given
                var log = new TestLog();
                // When
                log.Verbose("Hello World");
                // Then
                Assert.Equal(LogLevel.Verbose, log.Level);
            }
        }

        [Fact]
        public void Can_Write_Verbose_Message_With_Default_Verbosity3()
        {
            {
                // Given
                var log = new TestLog();
                // When
                log.Verbose("Hello World");
                // Then
                Assert.Equal("Hello World", log.Message);
            }
        }
    }
}
