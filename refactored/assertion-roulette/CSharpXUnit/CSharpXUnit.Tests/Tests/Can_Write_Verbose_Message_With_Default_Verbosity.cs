using Xunit;

public class LogExtensionsTests
{
    [Fact]
    public void can_Write_Verbose_Message_With_Default_Verbosity()
    {
        // Given
        TestLog log = new TestLog();
        // When
        log.Verbose("Hello World");
        // Then
        Assert.Equal(Verbosity.Verbose, log.GetVerbosity(), "Explanation message");
        Assert.Equal(LogLevel.Verbose, log.GetLevel(), "Explanation message");
        Assert.Equal("Hello World", log.GetMessage(), "Explanation message");
    }
}
