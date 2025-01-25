import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.concurrent.*;

public class CustomArgumentException {

    @Test
    @Disabled("")
    public void recursiveAsyncExceptionWithoutFlattenException() {
        int recursionCount = 3;
        Callable<Integer> innerAction = () -> { throw new ApplicationException("Life is hard"); };

        Future<Integer> t1 = Executors.newSingleThreadExecutor().submit(() -> {
            return nestedFunc(recursionCount, innerAction);
        });

        try {
            t1.get();
        } catch (ExecutionException ex) {
            ExceptionLayoutRenderer layoutRenderer = new ExceptionLayoutRenderer();
            layoutRenderer.setFormat("ToString");
            layoutRenderer.setFlattenException(false);

            LogEventInfo logEvent = LogEventInfo.create(LogLevel.ERROR, null, null, ex);
            String result = layoutRenderer.render(logEvent);

            int needleCount = 0;
            int foundIndex = result.indexOf("nestedFunc");
            while (foundIndex >= 0) {
                needleCount++;
                foundIndex = result.indexOf("nestedFunc", foundIndex + "nestedFunc".length());
            }

            assertTrue(needleCount >= recursionCount, needleCount + " too small");
        }
    }
}
