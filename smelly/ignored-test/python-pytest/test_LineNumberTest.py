import pytest

@pytest.mark.skip(reason="")
class TestCallSite:

    def test_line_number(self):
        log_factory = LogFactory().setup().load_configuration_from_xml("""
                <nlog>
                    <targets><target name='debug' type='Debug' layout='${callsite:filename=true} ${message}' /></targets>
                    <rules>
                        <logger name='*' minlevel='Debug' writeTo='debug' />
                    </rules>
                </nlog>""").log_factory
        logger = log_factory.get_logger("A")

        # Assuming there's DEBUG equivalent handling in Python
        logger.debug("msg")
        linenumber = get_prev_line_number()
        last_message = get_debug_last_message("debug", log_factory)
        
        assert "callsitetests.cs:" + str(linenumber) in last_message, \
            f"Invalid line number. Expected prefix of 10000, got: {last_message}"
