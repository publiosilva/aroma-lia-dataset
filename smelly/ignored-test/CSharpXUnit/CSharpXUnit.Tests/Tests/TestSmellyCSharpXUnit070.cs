using Xunit;

namespace DefaultNamespace
{
    public class CallSiteTests
    {
        [Fact(Skip = "")]
        public void GivenOneSkipFrameDefined_WhenLogging_ShouldSkipOneUserStackFrame()
        {
            {
                var logFactory = new LogFactory().Setup().LoadConfigurationFromXml(@"
                        <nlog>
                            <targets><target name='debug' type='Debug' layout='${callsite:skipframes=1} ${message}' /></targets>
                            <rules>
                                <logger name='*' minlevel='Debug' writeTo='debug' />
                            </rules>
                        </nlog>").LogFactory;
                var logger = logFactory.GetLogger("A");
                Action action = () => logger.Debug("msg");
                action.Invoke();
                logFactory.AssertDebugLastMessage("NLog.UnitTests.LayoutRenderers.CallSiteTests.GivenOneSkipFrameDefined_WhenLogging_ShouldSkipOneUserStackFrame msg");
            }
        }
    }
}
