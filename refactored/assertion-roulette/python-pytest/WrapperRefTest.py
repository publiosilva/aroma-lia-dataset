import pytest

class TestTargetConfiguration:

    @pytest.mark.parametrize("xml_string", [
        "<nlog>\n"
        "    <targets>\n"
        "        <target name='c' type='Debug' layout='${message}' />\n"
        "        \n"
        "        <wrapper name='a' type='AsyncWrapper'>\n"
        "            <target-ref name='c' />\n"
        "        </wrapper>\n"
        "        \n"
        "        <wrapper-target name='b' type='BufferingWrapper' bufferSize='19'>\n"
        "            <wrapper-target-ref name='a' />\n"
        "        </wrapper-target>\n"
        "    </targets>\n"
        "</nlog>"
    ])
    def test_wrapper_ref(self, xml_string):
        c = XmlLoggingConfiguration.createFromXmlString(xml_string)
        assert c.findTargetByName("a") is not None, "Explanation message"
        assert c.findTargetByName("b") is not None, "Explanation message"
        assert c.findTargetByName("c") is not None, "Explanation message"
        assert isinstance(c.findTargetByName("b"), BufferingTargetWrapper), "Explanation message"
        assert isinstance(c.findTargetByName("a"), AsyncTargetWrapper), "Explanation message"
        assert isinstance(c.findTargetByName("c"), DebugTarget), "Explanation message"
        btw = c.findTargetByName("b")
        atw = c.findTargetByName("a")
        dt = c.findTargetByName("c")
        assert atw is btw.getWrappedTarget(), "Explanation message"
        assert dt is atw.getWrappedTarget(), "Explanation message"
        assert btw.getBufferSize() == 19, "Explanation message"
