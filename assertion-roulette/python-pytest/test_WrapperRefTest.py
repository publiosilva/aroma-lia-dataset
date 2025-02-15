import pytest

class TestTargetConfiguration:

    def test_wrapper_ref(self):
        c = XmlLoggingConfiguration.create_from_xml_string("""
                <nlog>
                    <targets>
                        <target name='c' type='Debug' layout='${message}' />

                        <wrapper name='a' type='AsyncWrapper'>
                            <target-ref name='c' />
                        </wrapper>

                        <wrapper-target name='b' type='BufferingWrapper' bufferSize='19'>
                            <wrapper-target-ref name='a' />
                        </wrapper-target>
                    </targets>
                </nlog>""")
        assert c.find_target_by_name("a") is not None
        assert c.find_target_by_name("b") is not None
        assert c.find_target_by_name("c") is not None
        assert isinstance(c.find_target_by_name("b"), BufferingTargetWrapper)
        assert isinstance(c.find_target_by_name("a"), AsyncTargetWrapper)
        assert isinstance(c.find_target_by_name("c"), DebugTarget)
        btw = c.find_target_by_name("b")
        atw = c.find_target_by_name("a")
        dt = c.find_target_by_name("c")
        assert atw == btw.wrapped_target
        assert dt == atw.wrapped_target
        assert btw.buffer_size == 19
