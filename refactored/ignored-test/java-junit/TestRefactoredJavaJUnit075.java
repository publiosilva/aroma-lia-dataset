import org.junit.Test;
import static org.junit.Assert.assertTrue;

// @Ignore
public class CallSiteTests {

    @Test
    public void lineNumberTest() {
        {
            LogFactory logFactory = new LogFactory().setup().loadConfigurationFromXml("<nlog>\n" +
                    "<targets><target name='debug' type='Debug' layout='${callsite:filename=true} ${message}' /></targets>\n" +
                    "<rules>\n" +
                    "<logger name='*' minlevel='Debug' writeTo='debug' />\n" +
                    "</rules>\n" +
                    "</nlog>").getLogFactory();
            Logger logger = logFactory.getLogger("A");
            // The following lines are for simulating line number changes which can't be directly translated into Java
            // In an actual implementation, suitable methods would need to be created or utilized.
            logger.debug("msg");
            int linenumber = getPrevLineNumber();
            String lastMessage = getDebugLastMessage("debug", logFactory);
            assertTrue("Invalid line number. Expected prefix of 10000, got: " + lastMessage, 
                lastMessage.toLowerCase().contains("callsitetests.java:" + linenumber));
        }
    }
}
