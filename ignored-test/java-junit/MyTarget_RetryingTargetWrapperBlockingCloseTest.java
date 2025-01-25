import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertNotNull;

import java.util.ArrayList;
import java.util.List;

public class MyTarget {

    @Test
    @Disabled("")
    public void retryingTargetWrapperBlockingCloseTest() throws Exception {
        retryingIntegrationTest(3, () -> {
            MyTarget target = new MyTarget();
            target.setThrowExceptions(5);

            RetryingTargetWrapper wrapper = new RetryingTargetWrapper();
            wrapper.setWrappedTarget(target);
            wrapper.setRetryCount(10);
            wrapper.setRetryDelayMilliseconds(5000);

            AsyncTargetWrapper asyncWrapper = new AsyncTargetWrapper(wrapper);
            asyncWrapper.setTimeToSleepBetweenBatches(1);

            asyncWrapper.initialize(null);
            wrapper.initialize(null);
            target.initialize(null);

            List<Exception> exceptions = new ArrayList<>();
            LogEventInfo[] events = new LogEventInfo[] {
                new LogEventInfo(LogLevel.DEBUG, "Logger1", "Hello").withContinuation(exceptions::add),
                new LogEventInfo(LogLevel.INFO, "Logger1", "Hello").withContinuation(exceptions::add),
                new LogEventInfo(LogLevel.INFO, "Logger2", "Hello").withContinuation(exceptions::add),
            };

            asyncWrapper.writeAsyncLogEvents(events);
            Thread.sleep(50);
            asyncWrapper.close();
            wrapper.close();
            target.close();
            Thread.sleep(200);

            assertNotNull(exceptions.get(0));
        });
    }
}
