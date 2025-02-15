import org.junit.Test;

public class CallSiteTests {
    @Test
    public void dontCleanClassNamesOfAnonymousDelegatesTest() {
        {
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml(
                    "<nlog>" +
                    "<targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:CleanNamesOfAnonymousDelegates=false}' /></targets>" +
                    "<rules>" +
                    "<logger name='*' levels='Fatal' writeTo='debug' />" +
                    "</rules>" +
                    "</nlog>").logFactory();
            Logger logger = logFactory.getLogger("A");
            final boolean[] done = {false};
            new Thread(() -> {
                logger.fatal("message");
                done[0] = true;
            }).start();
            while (!done[0]) {
                try {
                    // Thread.sleep(10);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }

            if (done[0]) {
                logFactory.assertDebugLastMessageContains("+<>");
            }
        }
    }
}
