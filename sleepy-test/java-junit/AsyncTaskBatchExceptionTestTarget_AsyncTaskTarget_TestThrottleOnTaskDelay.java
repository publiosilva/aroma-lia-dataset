import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class AsyncTaskBatchExceptionTestTarget {

    @Test
    public void asyncTaskTarget_TestThrottleOnTaskDelay() throws Exception {
        AsyncTaskBatchTestTarget asyncTarget = new AsyncTaskBatchTestTarget();
        asyncTarget.setLayout("${level}");
        asyncTarget.setTaskDelayMilliseconds(50);
        asyncTarget.setBatchSize(10);

        Logger logger = new LogFactory().setup().loadConfiguration(builder -> {
            builder.forLogger().writeTo(asyncTarget);
        }).getCurrentClassLogger();

        for (int i = 0; i < 5; ++i) {
            for (int j = 0; j < 10; ++j) {
                logger.log(LogLevel.INFO, Integer.toString(i));
                Thread.sleep(20);
            }

            assertTrue(asyncTarget.waitForWriteEvent());
        }

        assertTrue(asyncTarget.getLogs().size() > 25, asyncTarget.getLogs().size() + " LogEvents are too few after " + asyncTarget.getWriteTasks() + " writes");
        assertTrue(asyncTarget.getWriteTasks() < 20, asyncTarget.getWriteTasks() + " writes are too many.");
    }
}
