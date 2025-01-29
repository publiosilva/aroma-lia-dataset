import pytest

class TestInternalLoggerTrace:

    @pytest.mark.skip(reason="")
    def test_should_throw_stack_overflow_exception_when_using_nlog_trace_listener(self):
        setup_test_configuration(NLogTraceListener, LogLevel.Trace, True, None)
        with pytest.raises(RecursionError):
            Trace.write_line("StackOverFlowException")
