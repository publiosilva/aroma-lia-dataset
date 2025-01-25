import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class AsyncRequestQueueTests {

    @Test
    public void asyncRequestQueueWithBlockBehavior() throws Exception {
        AsyncRequestQueue queue = new AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.BLOCK);
        CountDownLatch producerFinished = new CountDownLatch(1);
        int[] pushingEvent = {0};

        ExecutorService executor = Executors.newSingleThreadExecutor();
        executor.submit(() -> {
            try {
                for (int i = 0; i < 1000; ++i) {
                    AsyncLogEventInfo logEvent = LogEventInfo.createNullEvent().withContinuation(ex -> {});
                    logEvent.getLogEvent().setMessage("msg" + i);
                    pushingEvent[0] = i;
                    queue.enqueue(logEvent);
                }
            } finally {
                producerFinished.countDown();
            }
        });

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
        assertEquals(510, pushingEvent[0]);
        queue.dequeueBatch(1);
        total++;
        Thread.sleep(500);
        assertEquals(511, pushingEvent[0]);

        while (total < 1000) {
            int left = 1000 - total;
            logEventInfos = queue.dequeueBatch(left);
            int got = logEventInfos.length;
            assertTrue(got <= queue.getRequestLimit());
            total += got;
        }

        producerFinished.await();
        executor.shutdown();
    }
}
