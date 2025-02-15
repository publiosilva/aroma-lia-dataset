import pytest

class TestAsyncHelper:
    def test_continuation_error_timeout_not_hit(self):
        exceptions = []
        cont = AsyncHelpers.with_timeout(AsyncHelpers.prevent_multiple_calls(exceptions.append), 0.05)
        exception = Exception("Foo")
        cont(exception)
        time.sleep(0.1)
        assert len(exceptions) == 1
        assert exceptions[0] is not None
        assert exceptions[0] is exception
        cont(None)
        cont(Exception("Some exception"))
        cont(None)
        cont(Exception("Some exception"))
        assert len(exceptions) == 1
        assert exceptions[0] is not None
