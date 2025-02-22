import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class TargetConfigurationTests {

    @Test
    public void wrapperRefTest() {
        LoggingConfiguration c = XmlLoggingConfiguration.createFromXmlString(
                "<nlog>\n" +
                "    <targets>\n" +
                "        <target name='c' type='Debug' layout='${message}' />\n" +
                "        \n" +
                "        <wrapper name='a' type='AsyncWrapper'>\n" +
                "            <target-ref name='c' />\n" +
                "        </wrapper>\n" +
                "        \n" +
                "        <wrapper-target name='b' type='BufferingWrapper' bufferSize='19'>\n" +
                "            <wrapper-target-ref name='a' />\n" +
                "        </wrapper-target>\n" +
                "    </targets>\n" +
                "</nlog>");
        assertNotNull("Explanation message", c.findTargetByName("a"));
        assertNotNull("Explanation message", c.findTargetByName("b"));
        assertNotNull("Explanation message", c.findTargetByName("c"));
        assertTrue("Explanation message", c.findTargetByName("b") instanceof BufferingTargetWrapper);
        assertTrue("Explanation message", c.findTargetByName("a") instanceof AsyncTargetWrapper);
        assertTrue("Explanation message", c.findTargetByName("c") instanceof DebugTarget);
        BufferingTargetWrapper btw = (BufferingTargetWrapper) c.findTargetByName("b");
        AsyncTargetWrapper atw = (AsyncTargetWrapper) c.findTargetByName("a");
        DebugTarget dt = (DebugTarget) c.findTargetByName("c");
        assertSame("Explanation message", atw, btw.getWrappedTarget());
        assertSame("Explanation message", dt, atw.getWrappedTarget());
        assertEquals("Explanation message", 19, btw.getBufferSize());
    }
}
