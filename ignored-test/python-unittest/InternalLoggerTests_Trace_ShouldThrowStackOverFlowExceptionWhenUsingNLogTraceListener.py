import unittest

class TestInternalLoggerTrace(unittest.TestCase):
    @unittest.skip("")
    def test_should_throw_stack_overflow_exception_when_using_nlog_trace_listener(self):
        setup_test_configuration(NLogTraceListener, LogLevel.TRACE, True, None)
        with self.assertRaises(RecursionError):
            trace.write_line("StackOverFlowException")
