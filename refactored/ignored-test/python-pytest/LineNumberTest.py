import pytest

# @pytest.mark.skip
class TestCallSite:

    def test_line_number(self):
        log_factory = LogFactory().setup().load_configuration_from_xml("<nlog>\n" +
                                                                      "<targets><target name='debug' type='Debug' layout='${callsite:filename=true} ${message}' /></targets>\n" +
                                                                      "<rules>\n" +
                                                                      "<logger name='*' minlevel='Debug' writeTo='debug' />\n" +
                                                                      "</rules>\n" +
                                                                      "</nlog>").get_log_factory()
        logger = log_factory.get_logger("A")
        logger.debug("msg")
        linenumber = get_prev_line_number()
        last_message = get_debug_last_message("debug", log_factory)
        assert "Invalid line number. Expected prefix of 10000, got: " + last_message in last_message.lower()
