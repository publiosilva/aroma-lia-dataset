import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class AsyncRequestQueueEq {

    @Test
    public void logEvent_OutOfMemoryException_AsyncNotCrash() throws Exception {
        try (NoThrowNLogExceptions noThrow = new NoThrowNLogExceptions()) {
            LogFactory logFactory = new LogFactory().setup().loadConfiguration(builder -> {
                builder.forLogger().writeTo(new NoMemoryTarget() {{ setName("LowMem"); }}).withAsync();
            }).getLogFactory();
            Logger logger = logFactory.getCurrentClassLogger();
            NoMemoryTarget target = logFactory.getConfiguration().findTargetByName("LowMem");

            logger.info("Testing1");
            logFactory.flush();
            assertEquals(1, target.getFailedCount());

            logger.info("Testing2");
            for (int i = 0; i < 5000; ++i) {
                if (target.getWriteCount() != 1) break;
                Thread.sleep(1);
            }
            assertEquals(2, target.getFailedCount());

            target.setLowMemory(false);
            logger.info("Testing3");
            for (int i = 0; i < 5000; ++i) {
                if (target.getWriteCount() != 2) break;
                Thread.sleep(1);
            }
            assertEquals(2, target.getFailedCount());
            assertEquals(3, target.getWriteCount());
        }
    }
}
