def test_continuation_timeout_not_hit():
    exceptions = []
    cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.append), timedelta(milliseconds=50))
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
