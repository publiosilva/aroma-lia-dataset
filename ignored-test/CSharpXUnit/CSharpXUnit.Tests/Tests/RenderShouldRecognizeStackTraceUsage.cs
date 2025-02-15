using Xunit;

namespace DefaultNamespace
{
    public class LayoutTypedTests
    {
        [Fact(Skip = "")]
        public void RenderShouldRecognizeStackTraceUsage()
        {
            {
                // Arrange
                object[] callback_args = null;
                Action<LogEventInfo, object[]> callback = (evt, args) => callback_args = args;
                var logger = new LogFactory().Setup().LoadConfiguration(builder =>
                {
                    var methodCall = new NLog.Targets.MethodCallTarget("dbg", callback);
                    methodCall.Parameters.Add(new NLog.Targets.MethodCallParameter("LineNumber", "${callsite-linenumber}", typeof(int)));
                    builder.ForLogger().WriteTo(methodCall);
                }).GetLogger(nameof(RenderShouldRecognizeStackTraceUsage));
                // Act
                logger.Info("Testing");
                // Assert
                Assert.Single(callback_args);
                var lineNumber = Assert.IsType<int>(callback_args[0]);
                Assert.True(lineNumber > 0);
            }
        }
    }
}
