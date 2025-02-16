import org.junit.Test;
import static org.junit.Assert.assertTrue;

public class CallSiteTests {
    @Test
    public void dontCleanMethodNamesOfAnonymousDelegatesTest() {
        {
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml(
                "<nlog>" +
                "<targets><target name='debug' type='Debug' layout='${callsite:ClassName=false:CleanNamesOfAnonymousDelegates=false}' /></targets>" +
                "<rules>" +
                "<logger name='*' levels='Fatal' writeTo='debug' />" +
                "</rules>" +
                "</nlog>").getLogFactory();
            Logger logger = logFactory.getLogger("A");
            boolean[] done = {false};
            new Thread(() -> {
                logger.fatal("message");
                done[0] = true;
            }).start();
            while (!done[0]) {
                try {
                    // Thread.sleep(10);
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                }
            }
        
            if (done[0]) {
                String lastMessage = getDebugLastMessage("debug", logFactory);
                assertTrue(lastMessage.startsWith("<DontCleanMethodNamesOfAnonymousDelegatesTest>"));
            }
        }
    }
}
