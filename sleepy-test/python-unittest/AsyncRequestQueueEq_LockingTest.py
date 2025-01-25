import unittest
import threading
import time

class TestAsyncRequestQueueEq(unittest.TestCase):
    def test_locking(self):
        target = MyTarget()
        target.initialize(None)
        mre = threading.Event()
        background_thread_exception = None

        def background_thread():
            nonlocal background_thread_exception
            try:
                target.blocking_operation(500)
            except Exception as ex:
                background_thread_exception = ex
            finally:
                mre.set()

        t = threading.Thread(target=background_thread)
        target.initialize(None)
        t.start()
        time.sleep(0.05)

        exceptions = []
        target.write_async_log_event(LogEventInfo.create_null_event().with_continuation(exceptions.append))
        target.write_async_log_events([
            LogEventInfo.create_null_event().with_continuation(exceptions.append),
            LogEventInfo.create_null_event().with_continuation(exceptions.append),
        ])
        target.flush(exceptions.append)
        target.close()

        for ex in exceptions:
            self.assertIsNone(ex)

        mre.wait()
        if background_thread_exception is not None:
            self.fail(str(background_thread_exception))
