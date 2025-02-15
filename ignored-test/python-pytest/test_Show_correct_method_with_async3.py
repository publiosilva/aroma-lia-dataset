import pytest

class TestCallSite:
    @pytest.mark.skip(reason="")
    def test_show_correct_method_with_async3(self):
        current_method_full_name = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod3b"
        log_factory = LogFactory().setup().load_configuration_from_xml("""
               <nlog>
                   <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>
                   <rules>
                       <logger name='*' levels='Warn' writeTo='debug' />
                   </rules>
               </nlog>""").log_factory
        AsyncMethod3a(log_factory).wait()
        log_factory.assert_debug_last_message(f"{current_method_full_name}|direct")
        InnerClassAsyncMethod3().async_method3a(log_factory).wait()
        log_factory.assert_debug_last_message(f"{InnerClassAsyncMethod3.__str__()}.AsyncMethod3b|direct")
