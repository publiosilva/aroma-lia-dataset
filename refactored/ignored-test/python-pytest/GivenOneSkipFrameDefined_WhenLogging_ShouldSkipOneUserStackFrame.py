import pytest

class TestCallSite:

    # @pytest.mark.skip(reason="")
    def test_given_one_skip_frame_defined_when_logging_should_skip_one_user_stack_frame(self):
        log_factory = LogFactory().setup().load_configuration_from_xml(
            "<nlog>" +
            "<targets><target name='debug' type='Debug' layout='${callsite:skipframes=1} ${message}' /></targets>" +
            "<rules>" +
            "<logger name='*' minlevel='Debug' writeTo='debug' />" +
            "</rules>" +
            "</nlog>").log_factory
        logger = log_factory.get_logger("A")
        action = lambda: logger.debug("msg")
        action()
        log_factory.assert_debug_last_message("NLog.UnitTests.LayoutRenderers.CallSiteTests.GivenOneSkipFrameDefined_WhenLogging_ShouldSkipOneUserStackFrame msg")
