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
        assertNotNull(c.findTargetByName("a"));
        assertNotNull(c.findTargetByName("b"));
        assertNotNull(c.findTargetByName("c"));
        assertTrue(c.findTargetByName("b") instanceof BufferingTargetWrapper);
        assertTrue(c.findTargetByName("a") instanceof AsyncTargetWrapper);
        assertTrue(c.findTargetByName("c") instanceof DebugTarget);
        BufferingTargetWrapper btw = (BufferingTargetWrapper) c.findTargetByName("b");
        AsyncTargetWrapper atw = (AsyncTargetWrapper) c.findTargetByName("a");
        DebugTarget dt = (DebugTarget) c.findTargetByName("c");
        assertSame(atw, btw.getWrappedTarget());
        assertSame(dt, atw.getWrappedTarget());
        assertEquals(19, btw.getBufferSize());
    }
}
