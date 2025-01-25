using Xunit;

namespace None
{
    public class MyTarget
    {
        [Fact(Skip = "")]
        public void RetryingTargetWrapperBlockingCloseTest()
        {
            {
                RetryingIntegrationTest(3, () =>
                {
                    var target = new MyTarget()
                    {
                        ThrowExceptions = 5,
                    };
                    var wrapper = new RetryingTargetWrapper()
                    {
                        WrappedTarget = target,
                        RetryCount = 10,
                        RetryDelayMilliseconds = 5000,
                    };
                    var asyncWrapper = new AsyncTargetWrapper(wrapper)
                    {
                        TimeToSleepBetweenBatches = 1
                    };
                    asyncWrapper.Initialize(null);
                    wrapper.Initialize(null);
                    target.Initialize(null);
                    var exceptions = new List<Exception>();
                    var events = new[]
                    {
                        new LogEventInfo(LogLevel.Debug, "Logger1", "Hello").WithContinuation(exceptions.Add),
                        new LogEventInfo(LogLevel.Info, "Logger1", "Hello").WithContinuation(exceptions.Add),
                        new LogEventInfo(LogLevel.Info, "Logger2", "Hello").WithContinuation(exceptions.Add),
                    };
                    // Attempt to write LogEvents that will take forever to retry
                    asyncWrapper.WriteAsyncLogEvents(events);
                    // Wait a little for the AsyncWrapper to start writing
                    System.Threading.Thread.Sleep(50);
                    // Close down the AsyncWrapper while busy writing
                    asyncWrapper.Close();
                    // Close down the RetryingWrapper while busy retrying
                    wrapper.Close();
                    // Close down the actual target while busy writing
                    target.Close();
                    // Wait a little for the RetryingWrapper to detect that it has been closed down
                    System.Threading.Thread.Sleep(200);
                    // The premature abort, causes the exception to be logged
                    Assert.NotNull(exceptions[0]);
                });
            }
        }
    }
}
