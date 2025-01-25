import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;

public class AsyncRequestQueueEq {

    @Test
    public void lockingTest() throws Exception {
        MyTarget target = new MyTarget();
        target.initialize(null);
        CountDownLatch latch = new CountDownLatch(1);
        Exception[] backgroundThreadException = {null};

        Thread t = new Thread(() -> {
            try {
                target.blockingOperation(500);
            } catch (Exception ex) {
                backgroundThreadException[0] = ex;
            } finally {
                latch.countDown();
            }
        });

        target.initialize(null);
        t.start();
        Thread.sleep(50);

        List<Exception> exceptions = new ArrayList<>();
        target.writeAsyncLogEvent(LogEventInfo.createNullEvent().withContinuation(exceptions::add));
        target.writeAsyncLogEvents(new LogEventInfo[] {
            LogEventInfo.createNullEvent().withContinuation(exceptions::add),
            LogEventInfo.createNullEvent().withContinuation(exceptions::add)
        });
        target.flush(exceptions::add);
        target.close();

        exceptions.forEach(exception -> assertNull(exception));
        latch.await();

        if (backgroundThreadException[0] != null) {
            assertTrue(false, backgroundThreadException[0].toString());
        }
    }
}
