import org.junit.Test;
import org.junit.Ignore;

public class CallSiteTests {
  
    @Test
    // @Ignore("")

    public void logAfterAwait_ShouldCleanMethodNameAsync5() {
        {
            //namespace en name of current method
            final String currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod5";
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml("<nlog>\n" +
                "    <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>\n" +
                "    <rules>\n" +
                "        <logger name='*' levels='Debug' writeTo='debug' />\n" +
                "    </rules>\n" +
                "</nlog>").getLogFactory();
            AsyncMethod5(logFactory).get();
            logFactory.assertDebugLastMessage(currentMethodFullName + "|dude");
            new InnerClassAsyncMethod5().asyncMethod5(logFactory).get();
            logFactory.assertDebugLastMessage(InnerClassAsyncMethod5.class.toString() + ".asyncMethod5|dude");
        }
    }
}
