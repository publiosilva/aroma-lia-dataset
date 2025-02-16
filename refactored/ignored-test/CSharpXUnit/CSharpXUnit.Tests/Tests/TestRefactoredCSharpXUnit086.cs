using Xunit;

public class CallSiteTests
{
    [Fact]
    // [Fact(Skip = "")]

    public void show_correct_method_with_async3()
    {
        {
            string currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod3b";
            var logFactory = new LogFactory().setup().loadConfigurationFromXml(
                "<nlog>" +
                "<targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>" +
                "<rules>" +
                "<logger name='*' levels='Warn' writeTo='debug' />" +
                "</rules>" +
                "</nlog>").logFactory();
            AsyncMethod3a(logFactory).Wait();
            logFactory.assertDebugLastMessage(currentMethodFullName + "|direct");
            new InnerClassAsyncMethod3().asyncMethod3a(logFactory).Wait();
            logFactory.assertDebugLastMessage(typeof(InnerClassAsyncMethod3).ToString() + ".AsyncMethod3b|direct");
        }
    }
}
