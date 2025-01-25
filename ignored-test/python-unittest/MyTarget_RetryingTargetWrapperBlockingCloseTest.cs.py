import unittest

class TestMyTarget(unittest.TestCase):
    @unittest.skip("")
    def test_retrying_target_wrapper_blocking_close(self):
        retrying_integration_test(3, lambda: self._execute_test())

    def _execute_test(self):
        target = MyTarget()
        target.throw_exceptions = 5

        wrapper = RetryingTargetWrapper()
        wrapper.wrapped_target = target
        wrapper.retry_count = 10
        wrapper.retry_delay_milliseconds = 5000

        async_wrapper = AsyncTargetWrapper(wrapper)
        async_wrapper.time_to_sleep_between_batches = 1

        async_wrapper.initialize(None)
        wrapper.initialize(None)
        target.initialize(None)

        exceptions = []
        events = [
            LogEventInfo(LogLevel.DEBUG, "Logger1", "Hello").with_continuation(exceptions.append),
            LogEventInfo(LogLevel.INFO, "Logger1", "Hello").with_continuation(exceptions.append),
            LogEventInfo(LogLevel.INFO, "Logger2", "Hello").with_continuation(exceptions.append),
        ]

        async_wrapper.write_async_log_events(events)

        time.sleep(0.05)
        async_wrapper.close()
        wrapper.close()
        target.close()

        time.sleep(0.2)

        self.assertIsNotNone(exceptions[0])
