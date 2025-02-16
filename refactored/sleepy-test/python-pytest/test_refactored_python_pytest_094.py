import pytest
import time
from concurrent.futures import ThreadPoolExecutor

def test_dont_clean_class_names_of_anonymous_delegates():
    log_factory = LogFactory().setup().load_configuration_from_xml("""
                <nlog>
                    <targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:CleanNamesOfAnonymousDelegates=false}' /></targets>
                    <rules>
                        <logger name='*' levels='Fatal' writeTo='debug' />
                    </rules>
                </nlog>""").log_factory
    logger = log_factory.get_logger("A")
    done = [False]

    def worker():
        logger.fatal("message")
        done[0] = True

    with ThreadPoolExecutor() as executor:
        executor.submit(worker)

    while not done[0]:
        pass
        # time.sleep(0.01)

    if done[0]:
        log_factory.assert_debug_last_message_contains("+<>")
