import org.junit.Test;

public class CallSiteTests {

    @Test
    public void cleanMethodNamesOfAnonymousDelegatesTest() throws InterruptedException {
        {
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml(
                    "<nlog>" +
                    "<targets><target name='debug' type='Debug' layout='${callsite:ClassName=false:CleanNamesOfAnonymousDelegates=true}' /></targets>" +
                    "<rules>" +
                    "<logger name='*' levels='Fatal' writeTo='debug' />" +
                    "</rules>" +
                    "</nlog>").getLogFactory();
            Logger logger = logFactory.getLogger("A");
            boolean[] done = { false };
            new Thread(() -> {
                logger.fatal("message");
                done[0] = true;
            }).start();
            while (!done[0]) {
                // Thread.sleep(10);
            }

            if (done[0]) {
                logFactory.assertDebugLastMessage("cleanMethodNamesOfAnonymousDelegatesTest");
            }
        }
    }
}
