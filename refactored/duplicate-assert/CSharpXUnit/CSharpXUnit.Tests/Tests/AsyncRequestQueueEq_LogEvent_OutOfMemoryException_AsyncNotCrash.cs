using Xunit;

public class AsyncRequestQueueEq
{
    [Fact]
    public void LogEvent_OutOfMemoryException_AsyncNotCrash1()
    {
        using (var noThrowNLogExceptions = new NoThrowNLogExceptions())
        {
            var logFactory = new LogFactory().Setup().LoadConfiguration(builder =>
            {
                builder.ForLogger().WriteTo(new NoMemoryTarget() { Name = "LowMem" }).WithAsync();
            }).GetLogFactory();
            var logger = logFactory.GetCurrentClassLogger();
            var target = logFactory.GetConfiguration().FindTargetByName("LowMem");
            logger.Info("Testing1");
            logFactory.Flush();
            Assert.Equal(1, target.GetFailedCount());
            logger.Info("Testing2");
            for (int i = 0; i < 5000; ++i)
            {
                if (target.GetWriteCount() != 1)
                    break;
                System.Threading.Thread.Sleep(1);
            }

            Assert.Equal(2, target.GetFailedCount());
        }
    }

    [Fact]
    public void LogEvent_OutOfMemoryException_AsyncNotCrash2()
    {
        using (var noThrowNLogExceptions = new NoThrowNLogExceptions())
        {
            var logFactory = new LogFactory().Setup().LoadConfiguration(builder =>
            {
                builder.ForLogger().WriteTo(new NoMemoryTarget() { Name = "LowMem" }).WithAsync();
            }).GetLogFactory();
            var logger = logFactory.GetCurrentClassLogger();
            var target = logFactory.GetConfiguration().FindTargetByName("LowMem");
            logger.Info("Testing1");
            logFactory.Flush();
            Assert.Equal(1, target.GetFailedCount());
            logger.Info("Testing2");
            for (int i = 0; i < 5000; ++i)
            {
                if (target.GetWriteCount() != 1)
                    break;
                System.Threading.Thread.Sleep(1);
            }

            target.SetLowMemory(false);
            logger.Info("Testing3");
            for (int i = 0; i < 5000; ++i)
            {
                if (target.GetWriteCount() != 2)
                    break;
                System.Threading.Thread.Sleep(1);
            }

            Assert.Equal(2, target.GetFailedCount());
            Assert.Equal(3, target.GetWriteCount());
        }
    }
}
