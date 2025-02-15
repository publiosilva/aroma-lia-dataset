using Xunit;

namespace nlog
{
    public class AsyncRequestQueueEq
    {
        [Fact]
        public void LogEvent_OutOfMemoryException_AsyncNotCrash()
        {
            {
                using (new NoThrowNLogExceptions())
                {
                    var logFactory = new LogFactory().Setup().LoadConfiguration(builder =>
                    {
                        builder.ForLogger().WriteTo(new NoMemoryTarget() { Name = "LowMem" }).WithAsync();
                    }).LogFactory;
                    var logger = logFactory.GetCurrentClassLogger();
                    var target = logFactory.Configuration.FindTargetByName<NoMemoryTarget>("LowMem");
                    logger.Info("Testing1");
                    logFactory.Flush();
                    Assert.Equal(1, target.FailedCount);
                    logger.Info("Testing2");
                    for (int i = 0; i < 5000; ++i)
                    {
                        if (target.WriteCount != 1)
                            break;
                        Thread.Sleep(1);
                    }
            
                    Assert.Equal(2, target.FailedCount);
                    target.LowMemory = false;
                    logger.Info("Testing3");
                    for (int i = 0; i < 5000; ++i)
                    {
                        if (target.WriteCount != 2)
                            break;
                        Thread.Sleep(1);
                    }
            
                    Assert.Equal(2, target.FailedCount);
                    Assert.Equal(3, target.WriteCount);
                }
            }
        }
    }
}
