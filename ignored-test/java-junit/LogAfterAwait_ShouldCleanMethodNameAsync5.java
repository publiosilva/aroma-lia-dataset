import org.junit.Test;
import org.junit.Ignore;

public class CallSiteTests {
  
    @Test
    @Ignore("")

    public void logAfterAwait_ShouldCleanMethodNameAsync5() {
        {
            //namespace en name of current method
            final String currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod5";
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml(@"
                   <nlog>
                       <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>
                       <rules>
                           <logger name='*' levels='Debug' writeTo='debug' />
                       </rules>
                   </nlog>").getLogFactory();
            AsyncMethod5(logFactory).get();
            logFactory.assertDebugLastMessage(currentMethodFullName + "|dude");
            new InnerClassAsyncMethod5().asyncMethod5(logFactory).get();
            logFactory.assertDebugLastMessage(InnerClassAsyncMethod5.class.toString() + ".asyncMethod5|dude");
        }
    }
}
