import pytest

class TestAsyncHelper:
    
    def test_continuation_timeout_not_hit(self):
        exceptions = []
        cont = AsyncHelpers.with_timeout(AsyncHelpers.prevent_multiple_calls(exceptions.append), 0.05)
        cont(None)
        time.sleep(0.1)
        assert len(exceptions) == 1
        assert exceptions[0] is None
        cont(None)
        cont(ApplicationException("Some exception"))
        cont(None)
        cont(ApplicationException("Some exception"))
        assert len(exceptions) == 1
        assert exceptions[0] is None
