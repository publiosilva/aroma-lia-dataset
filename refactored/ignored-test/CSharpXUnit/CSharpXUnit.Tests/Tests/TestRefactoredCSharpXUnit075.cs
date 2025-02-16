using Xunit;
using static Xunit.Assert;

public class CallSiteTests
{
    [Fact]
    // [Fact(Skip = "Disabled")]
    public void LineNumberTest()
    {
        {
            var logFactory = new LogFactory().Setup().LoadConfigurationFromXml("<nlog>\n" +
                    "<targets><target name='debug' type='Debug' layout='${callsite:filename=true} ${message}' /></targets>\n" +
                    "<rules>\n" +
                    "<logger name='*' minlevel='Debug' writeTo='debug' />\n" +
                    "</rules>\n" +
                    "</nlog>").GetLogFactory();
            var logger = logFactory.GetLogger("A");
            // The following lines are for simulating line number changes which can't be directly translated into C#
            // In an actual implementation, suitable methods would need to be created or utilized.
            logger.Debug("msg");
            int linenumber = GetPrevLineNumber();
            string lastMessage = GetDebugLastMessage("debug", logFactory);
            True(lastMessage.ToLower().Contains("callsiteTests.cs:" + linenumber), "Invalid line number. Expected prefix of 10000, got: " + lastMessage);
        }
    }
}
