import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.Test;

public class AsyncRequestQueueTests {

    @Test
    public void asyncRequestQueueWithBlockBehavior1() throws InterruptedException {
        {
            AsyncRequestQueue queue = new AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block);
            CountDownLatch producerFinished = new CountDownLatch(1);
            int[] pushingEvent = {0};
            Thread producer = new Thread(() -> {
                // producer thread
                for (int i = 0; i < 1000; ++i) {
                    AsyncLogEventInfo logEvent = LogEventInfo.createNullEvent().withContinuation(ex -> {
                    });
                    logEvent.getLogEvent().setMessage("msg" + i);
                    pushingEvent[0] = i;
                    queue.enqueue(logEvent);
                }

                producerFinished.countDown();
            });
            producer.start();
            // consumer thread
            AsyncLogEventInfo[] logEventInfos;
            int total = 0;
            while (total < 500) {
                int left = 500 - total;
                logEventInfos = queue.dequeueBatch(left);
                int got = logEventInfos.length;
                assertTrue(got <= queue.getRequestLimit());
                total += got;
            }

            Thread.sleep(500);
            // producer is blocked on trying to push event #510
            assertEquals(510, pushingEvent[0]);
            queue.dequeueBatch(1);
            total++;
            Thread.sleep(500);
            // producer is now blocked on trying to push event #511
            assertEquals(511, pushingEvent[0]);
            while (total < 1000) {
                int left = 1000 - total;
                logEventInfos = queue.dequeueBatch(left);
                int got = logEventInfos.length;
                total += got;
            }

            // producer should now finish
            producerFinished.await();
        }
    }

    @Test
    public void asyncRequestQueueWithBlockBehavior2() throws InterruptedException {
        {
            AsyncRequestQueue queue = new AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block);
            CountDownLatch producerFinished = new CountDownLatch(1);
            int[] pushingEvent = {0};
            Thread producer = new Thread(() -> {
                // producer thread
                for (int i = 0; i < 1000; ++i) {
                    AsyncLogEventInfo logEvent = LogEventInfo.createNullEvent().withContinuation(ex -> {
                    });
                    logEvent.getLogEvent().setMessage("msg" + i);
                    pushingEvent[0] = i;
                    queue.enqueue(logEvent);
                }

                producerFinished.countDown();
            });
            producer.start();
            // consumer thread
            AsyncLogEventInfo[] logEventInfos;
            int total = 0;
            while (total < 500) {
                int left = 500 - total;
                logEventInfos = queue.dequeueBatch(left);
                int got = logEventInfos.length;
                total += got;
            }

            Thread.sleep(500);
            // producer is blocked on trying to push event #510
            assertEquals(510, pushingEvent[0]);
            queue.dequeueBatch(1);
            total++;
            Thread.sleep(500);
            // producer is now blocked on trying to push event #511
            assertEquals(511, pushingEvent[0]);
            while (total < 1000) {
                int left = 1000 - total;
                logEventInfos = queue.dequeueBatch(left);
                int got = logEventInfos.length;
                assertTrue(got <= queue.getRequestLimit());
                total += got;
            }

            // producer should now finish
            producerFinished.await();
        }
    }
}
