using Xunit;

namespace nlog
{
    public class AsyncRequestQueueTests
    {
        [Fact]
        public void AsyncRequestQueueWithBlockBehavior()
        {
            {
                var queue = new AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block);
                ManualResetEvent producerFinished = new ManualResetEvent(false);
                int pushingEvent = 0;
                ThreadPool.QueueUserWorkItem(s =>
                {
                    // producer thread
                    for (int i = 0; i < 1000; ++i)
                    {
                        AsyncLogEventInfo logEvent = LogEventInfo.CreateNullEvent().WithContinuation(ex =>
                        {
                        });
                        logEvent.LogEvent.Message = "msg" + i;
                        // Console.WriteLine("Pushing event {0}", i);
                        pushingEvent = i;
                        queue.Enqueue(logEvent);
                    }
            
                    producerFinished.Set();
                });
                // consumer thread
                AsyncLogEventInfo[] logEventInfos;
                int total = 0;
                while (total < 500)
                {
                    int left = 500 - total;
                    logEventInfos = queue.DequeueBatch(left);
                    int got = logEventInfos.Length;
                    Assert.True(got <= queue.RequestLimit);
                    total += got;
                }
            
                Thread.Sleep(500);
                // producer is blocked on trying to push event #510
                Assert.Equal(510, pushingEvent);
                queue.DequeueBatch(1);
                total++;
                Thread.Sleep(500);
                // producer is now blocked on trying to push event #511
                Assert.Equal(511, pushingEvent);
                while (total < 1000)
                {
                    int left = 1000 - total;
                    logEventInfos = queue.DequeueBatch(left);
                    int got = logEventInfos.Length;
                    Assert.True(got <= queue.RequestLimit);
                    total += got;
                }
            
                // producer should now finish
                producerFinished.WaitOne();
            }
        }
    }
}
