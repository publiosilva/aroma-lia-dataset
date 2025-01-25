using Xunit;

namespace nlog
{
    public class AsyncTaskBatchExceptionTestTarget
    {
        [Fact]
        public void AsyncTaskTarget_TestThrottleOnTaskDelay()
        {
            {
                var asyncTarget = new AsyncTaskBatchTestTarget
                {
                    Layout = "${level}",
                    TaskDelayMilliseconds = 50,
                    BatchSize = 10,
                };
                var logger = new LogFactory().Setup().LoadConfiguration(builder =>
                {
                    builder.ForLogger().WriteTo(asyncTarget);
                }).GetCurrentClassLogger();
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < 10; ++j)
                    {
                        logger.Log(LogLevel.Info, i.ToString());
                        Thread.Sleep(20);
                    }
            
                    Assert.True(asyncTarget.WaitForWriteEvent());
                }
            
                Assert.True(asyncTarget.Logs.Count > 25, $"{asyncTarget.Logs.Count} LogEvents are too few after {asyncTarget.WriteTasks} writes");
                Assert.True(asyncTarget.WriteTasks < 20, $"{asyncTarget.WriteTasks} writes are too many.");
            }
        }
    }
}
