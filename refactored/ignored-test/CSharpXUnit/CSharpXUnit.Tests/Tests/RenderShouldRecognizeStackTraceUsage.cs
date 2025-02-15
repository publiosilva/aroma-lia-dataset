using Xunit;

public class LayoutTypedTests
{
    [Fact]
    // [Fact(Skip = "Disabled")]
    public void RenderShouldRecognizeStackTraceUsage()
    {
        // Arrange
        object[] callbackArgs = null;
        Action<LogEventInfo, object[]> callback = (evt, args) => callbackArgs = args;
        Logger logger = new LogFactory().Setup().LoadConfiguration(builder => {
            MethodCallTarget methodCall = new MethodCallTarget("dbg", callback);
            methodCall.Parameters.Add(new MethodCallParameter("LineNumber", "${callsite-linenumber}", typeof(int)));
            builder.ForLogger().WriteTo(methodCall);
        }).GetLogger("RenderShouldRecognizeStackTraceUsage");
        
        // Act
        logger.Info("Testing");
        
        // Assert
        Assert.Equal(1, callbackArgs.Length);
        int lineNumber = (int)callbackArgs[0];
        Assert.True(lineNumber > 0);
    }
}
