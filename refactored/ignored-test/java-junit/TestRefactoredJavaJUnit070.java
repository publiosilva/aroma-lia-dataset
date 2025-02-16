import org.junit.Test;
import org.junit.Ignore;

public class CallSiteTests {

    @Test
    // @Ignore("")

    public void GivenOneSkipFrameDefined_WhenLogging_ShouldSkipOneUserStackFrame() {
        {
            LogFactory logFactory = new LogFactory().Setup().LoadConfigurationFromXml(
                    "<nlog>" +
                    "<targets><target name='debug' type='Debug' layout='${callsite:skipframes=1} ${message}' /></targets>" +
                    "<rules>" +
                    "<logger name='*' minlevel='Debug' writeTo='debug' />" +
                    "</rules>" +
                    "</nlog>").LogFactory;
            Logger logger = logFactory.GetLogger("A");
            Runnable action = () -> logger.Debug("msg");
            action.run();
            logFactory.AssertDebugLastMessage("NLog.UnitTests.LayoutRenderers.CallSiteTests.GivenOneSkipFrameDefined_WhenLogging_ShouldSkipOneUserStackFrame msg");
        }
    }
}
