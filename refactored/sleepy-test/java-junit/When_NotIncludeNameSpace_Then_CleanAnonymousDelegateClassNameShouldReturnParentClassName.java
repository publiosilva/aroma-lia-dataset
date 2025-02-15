import org.junit.Test;
import static org.junit.Assert.*;

public class CallSiteTests {
    
    @Test
    public void when_NotIncludeNameSpace_then_CleanAnonymousDelegateClassNameShouldReturnParentClassName() throws InterruptedException {
        LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml(
                "<nlog>" +
                        "<targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:IncludeNamespace=false:CleanNamesOfAnonymousDelegates=true}' /></targets>" +
                        "<rules>" +
                        "<logger name='*' levels='Fatal' writeTo='debug' />" +
                        "</rules>" +
                        "</nlog>"
        ).logFactory();
        Logger logger = logFactory.getLogger("A");
        final boolean[] done = {false};
        Thread thread = new Thread(() -> {
            logger.fatal("message");
            done[0] = true;
        });
        thread.start();
        while (!done[0]) {
            // Thread.sleep(10);
        }

        if (done[0]) {
            logFactory.assertDebugLastMessage("CallSiteTests");
        }
    }
}
