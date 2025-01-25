using Xunit;

namespace nlog
{
    public class AsyncRequestQueueEq
    {
        [Fact]
        public void LockingTest()
        {
            {
                var target = new MyTarget();
                target.Initialize(null);
                var mre = new ManualResetEvent(false);
                Exception backgroundThreadException = null;
                Thread t = new Thread(() =>
                {
                    try
                    {
                        target.BlockingOperation(500);
                    }
                    catch (Exception ex)
                    {
                        backgroundThreadException = ex;
                    }
                    finally
                    {
                        mre.Set();
                    }
                });
                target.Initialize(null);
                t.Start();
                Thread.Sleep(50);
                List<Exception> exceptions = new List<Exception>();
                target.WriteAsyncLogEvent(LogEventInfo.CreateNullEvent().WithContinuation(exceptions.Add));
                target.WriteAsyncLogEvents(new[] { LogEventInfo.CreateNullEvent().WithContinuation(exceptions.Add), LogEventInfo.CreateNullEvent().WithContinuation(exceptions.Add), });
                target.Flush(exceptions.Add);
                target.Close();
                exceptions.ForEach(Assert.Null);
                mre.WaitOne();
                if (backgroundThreadException != null)
                {
                    Assert.True(false, backgroundThreadException.ToString());
                }
            }
        }
    }
}
