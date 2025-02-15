using System.Threading;
using Xunit;

public class AsyncRequestQueueTests
{
    [Fact]
    public void asyncRequestQueueWithBlockBehavior1()
    {
        AsyncRequestQueue queue = new AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block);
        CountdownEvent producerFinished = new CountdownEvent(1);
        int[] pushingEvent = { 0 };
        Thread producer = new Thread(() => {
            for (int i = 0; i < 1000; ++i)
            {
                AsyncLogEventInfo logEvent = LogEventInfo.CreateNullEvent().WithContinuation(ex => { });
                logEvent.GetLogEvent().SetMessage("msg" + i);
                pushingEvent[0] = i;
                queue.Enqueue(logEvent);
            }

            producerFinished.Signal();
        });
        producer.Start();
        AsyncLogEventInfo[] logEventInfos;
        int total = 0;
        while (total < 500)
        {
            int left = 500 - total;
            logEventInfos = queue.DequeueBatch(left);
            int got = logEventInfos.Length;
            Assert.True(got <= queue.GetRequestLimit());
            total += got;
        }

        Thread.Sleep(500);
        Assert.Equal(510, pushingEvent[0]);
        queue.DequeueBatch(1);
        total++;
        Thread.Sleep(500);
        Assert.Equal(511, pushingEvent[0]);
        while (total < 1000)
        {
            int left = 1000 - total;
            logEventInfos = queue.DequeueBatch(left);
            int got = logEventInfos.Length;
            total += got;
        }

        producerFinished.Wait();
    }

    [Fact]
    public void asyncRequestQueueWithBlockBehavior2()
    {
        AsyncRequestQueue queue = new AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block);
        CountdownEvent producerFinished = new CountdownEvent(1);
        int[] pushingEvent = { 0 };
        Thread producer = new Thread(() => {
            for (int i = 0; i < 1000; ++i)
            {
                AsyncLogEventInfo logEvent = LogEventInfo.CreateNullEvent().WithContinuation(ex => { });
                logEvent.GetLogEvent().SetMessage("msg" + i);
                pushingEvent[0] = i;
                queue.Enqueue(logEvent);
            }

            producerFinished.Signal();
        });
        producer.Start();
        AsyncLogEventInfo[] logEventInfos;
        int total = 0;
        while (total < 500)
        {
            int left = 500 - total;
            logEventInfos = queue.DequeueBatch(left);
            int got = logEventInfos.Length;
            total += got;
        }

        Thread.Sleep(500);
        Assert.Equal(510, pushingEvent[0]);
        queue.DequeueBatch(1);
        total++;
        Thread.Sleep(500);
        Assert.Equal(511, pushingEvent[0]);
        while (total < 1000)
        {
            int left = 1000 - total;
            logEventInfos = queue.DequeueBatch(left);
            int got = logEventInfos.Length;
            Assert.True(got <= queue.GetRequestLimit());
            total += got;
        }

        producerFinished.Wait();
    }
}
