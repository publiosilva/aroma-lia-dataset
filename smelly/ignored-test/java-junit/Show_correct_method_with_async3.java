import org.junit.Test;
import org.junit.Ignore;

public class CallSiteTests {
    
    @Test
    @Ignore("")

    public void show_correct_method_with_async3() {
        {
            String currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod3b";
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml(
                "<nlog>" +
                "<targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>" +
                "<rules>" +
                "<logger name='*' levels='Warn' writeTo='debug' />" +
                "</rules>" +
                "</nlog>").logFactory();
            AsyncMethod3a(logFactory).wait();
            logFactory.assertDebugLastMessage(currentMethodFullName + "|direct");
            new InnerClassAsyncMethod3().asyncMethod3a(logFactory).wait();
            logFactory.assertDebugLastMessage(InnerClassAsyncMethod3.class.toString() + ".AsyncMethod3b|direct");
        }
    }
}
