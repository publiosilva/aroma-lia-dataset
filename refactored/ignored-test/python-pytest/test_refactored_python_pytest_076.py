import pytest

class TestCallSite:

    # @pytest.mark.skip(reason="")
    def test_log_after_await_should_clean_method_name_async5(self):
        current_method_full_name = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod5"
        log_factory = LogFactory().setup().load_configuration_from_xml("<nlog>\n" +
            "    <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>\n" +
            "    <rules>\n" +
            "        <logger name='*' levels='Debug' writeTo='debug' />\n" +
            "    </rules>\n" +
            "</nlog>").get_log_factory()
        AsyncMethod5(log_factory).get()
        log_factory.assert_debug_last_message(current_method_full_name + "|dude")
        InnerClassAsyncMethod5().async_method5(log_factory).get()
        log_factory.assert_debug_last_message(f"{InnerClassAsyncMethod5.__class__}|dude")
