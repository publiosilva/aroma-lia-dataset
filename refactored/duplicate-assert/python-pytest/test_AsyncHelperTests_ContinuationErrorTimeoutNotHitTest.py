import time
import pytest

class TestAsyncHelper:
    def test_continuation_error_timeout_not_hit_1(self):
        exceptions = []
        cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.append), 50)
        exception = ApplicationException("Foo")
        cont.apply(exception)
        time.sleep(0.1)
        assert len(exceptions) == 1
        assert exceptions[0] is not None
        assert exceptions[0] is exception

    def test_continuation_error_timeout_not_hit_2(self):
        exceptions = []
        cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.append), 50)
        exception = ApplicationException("Foo")
        cont.apply(exception)
        time.sleep(0.1)
        cont.apply(None)
        cont.apply(ApplicationException("Some exception"))
        cont.apply(None)
        cont.apply(ApplicationException("Some exception"))
        assert len(exceptions) == 1
        assert exceptions[0] is not None
