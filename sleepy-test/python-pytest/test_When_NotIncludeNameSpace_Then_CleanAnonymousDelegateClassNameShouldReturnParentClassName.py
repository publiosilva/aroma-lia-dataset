import pytest
import time
from threading import Thread

class TestCallSite:
    def test_when_not_include_namespace_then_clean_anonymous_delegate_class_name_should_return_parent_class_name(self):
        log_factory = LogFactory().setup().load_configuration_from_xml("""
                    <nlog>
                        <targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:IncludeNamespace=false:CleanNamesOfAnonymousDelegates=true}' /></targets>
                        <rules>
                            <logger name='*' levels='Fatal' writeTo='debug' />
                        </rules>
                    </nlog>""").log_factory
        logger = log_factory.get_logger("A")
        done = False
        
        def worker():
            nonlocal done
            logger.fatal("message")
            done = True
            
        thread = Thread(target=worker)
        thread.start()
        
        while not done:
            time.sleep(0.01)
        
        if done:
            log_factory.assert_debug_last_message("CallSiteTests")
