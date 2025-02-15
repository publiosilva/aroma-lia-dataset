using Xunit;

namespace DefaultNamespace
{
    public class CallSiteTests
    {
        [Fact(Skip = "")]
        public void LineNumberTest()
        {
            {
                var logFactory = new LogFactory().Setup().LoadConfigurationFromXml(@"
                        <nlog>
                            <targets><target name='debug' type='Debug' layout='${callsite:filename=true} ${message}' /></targets>
                            <rules>
                                <logger name='*' minlevel='Debug' writeTo='debug' />
                            </rules>
                        </nlog>").LogFactory;
                var logger = logFactory.GetLogger("A");
            #if DEBUG
            #line 100000
            #endif
                logger.Debug("msg");
                var linenumber = GetPrevLineNumber();
                string lastMessage = GetDebugLastMessage("debug", logFactory);
                // There's a difference in handling line numbers between .NET and Mono
                // We're just interested in checking if it's above 100000
                Assert.True(lastMessage.IndexOf("callsitetests.cs:" + linenumber, StringComparison.OrdinalIgnoreCase) >= 0, "Invalid line number. Expected prefix of 10000, got: " + lastMessage);
            #if DEBUG
            #line default
            #endif
            }
        }
    }
}
