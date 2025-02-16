using Xunit;

public class CallSiteTests {
  
    [Fact]
    // [Fact(Skip = "")]

    public void logAfterAwait_ShouldCleanMethodNameAsync5() {
        {
            //namespace en name of current method
            var currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod5";
            var logFactory = new LogFactory().Setup().LoadConfigurationFromXml("<nlog>\n" +
                "    <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>\n" +
                "    <rules>\n" +
                "        <logger name='*' levels='Debug' writeTo='debug' />\n" +
                "    </rules>\n" +
                "</nlog>").GetLogFactory();
            AsyncMethod5(logFactory).GetAwaiter().GetResult();
            logFactory.AssertDebugLastMessage(currentMethodFullName + "|dude");
            new InnerClassAsyncMethod5().AsyncMethod5(logFactory).GetAwaiter().GetResult();
            logFactory.AssertDebugLastMessage(typeof(InnerClassAsyncMethod5).ToString() + ".AsyncMethod5|dude");
        }
    }
}
