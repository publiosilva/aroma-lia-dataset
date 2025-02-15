import pytest
import time
from threading import Thread

def test_clean_class_names_of_anonymous_delegates():
    log_factory = LogFactory().setup().load_configuration_from_xml("""
                <nlog>
                    <targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:CleanNamesOfAnonymousDelegates=true}' /></targets>
                    <rules>
                        <logger name='*' levels='Fatal' writeTo='debug' />
                    </rules>
                </nlog>""").log_factory
    logger = log_factory.get_logger("A")
    done = [False]

    def worker():
        logger.fatal("message")
        done[0] = True

    thread = Thread(target=worker)
    thread.start()

    while not done[0]:
        # time.sleep(0.01)
        pass

    if done[0]:
        log_factory.assert_debug_last_message("NLog.UnitTests.LayoutRenderers.CallSiteTests")
