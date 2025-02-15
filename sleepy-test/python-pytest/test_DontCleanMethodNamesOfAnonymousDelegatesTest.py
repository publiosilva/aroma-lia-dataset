import pytest
import time
from threading import Thread

def test_dont_clean_method_names_of_anonymous_delegates():
    log_factory = LogFactory().setup().load_configuration_from_xml("""
                            <nlog>
                                <targets><target name='debug' type='Debug' layout='${callsite:ClassName=false:CleanNamesOfAnonymousDelegates=false}' /></targets>
                                <rules>
                                    <logger name='*' levels='Fatal' writeTo='debug' />
                                </rules>
                            </nlog>""").log_factory
    logger = log_factory.get_logger("A")
    done = [False]  # Use a list to share state between threads
    
    def worker():
        logger.fatal("message")
        done[0] = True
    
    thread = Thread(target=worker)
    thread.start()
    
    while not done[0]:
        time.sleep(0.01)
    
    if done[0]:
        last_message = get_debug_last_message("debug", log_factory)
        assert last_message.startswith("<DontCleanMethodNamesOfAnonymousDelegatesTest>")
