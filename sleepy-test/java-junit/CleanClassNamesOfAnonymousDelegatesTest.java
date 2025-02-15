import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.Test;
import java.util.concurrent.atomic.AtomicBoolean;

public class CallSiteTests {

    @Test
    public void cleanClassNamesOfAnonymousDelegatesTest() {
        var logFactory = new LogFactory().setup().loadConfigurationFromXml(
                "<nlog>" +
                "<targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:CleanNamesOfAnonymousDelegates=true}' /></targets>" +
                "<rules>" +
                "<logger name='*' levels='Fatal' writeTo='debug' />" +
                "</rules>" +
                "</nlog>").logFactory();
        var logger = logFactory.getLogger("A");
        AtomicBoolean done = new AtomicBoolean(false);
        Thread thread = new Thread(() -> {
            logger.fatal("message");
            done.set(true);
        });
        thread.start();
        
        while (!done.get()) {
            try {
                Thread.sleep(10);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }

        if (done.get()) {
            logFactory.assertDebugLastMessage("NLog.UnitTests.LayoutRenderers.CallSiteTests");
        }
    }
}
