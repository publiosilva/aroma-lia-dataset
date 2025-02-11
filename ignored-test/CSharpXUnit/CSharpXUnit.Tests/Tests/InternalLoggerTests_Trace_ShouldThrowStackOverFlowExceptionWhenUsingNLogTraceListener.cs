using Xunit;

namespace None
{
    public class InternalLoggerTests_Trace
    {
        [Fact(Skip = "")]
        public void ShouldThrowStackOverFlowExceptionWhenUsingNLogTraceListener()
        {
            {
                SetupTestConfiguration<NLogTraceListener>(LogLevel.Trace, true, null);
                Assert.Throws<StackOverflowException>(() => Trace.WriteLine("StackOverFlowException"));
            }
        }
    }
}
