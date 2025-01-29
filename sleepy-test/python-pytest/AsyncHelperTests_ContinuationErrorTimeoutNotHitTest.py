def test_continuation_error_timeout_not_hit():
    exceptions = []
    cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.append), timedelta(milliseconds=50))
    exception = ApplicationException("Foo")
    cont(exception)
    time.sleep(0.1)
    assert len(exceptions) == 1
    assert exceptions[0] is not None
    assert exceptions[0] is exception
    cont(None)
    cont(ApplicationException("Some exception"))
    cont(None)
    cont(ApplicationException("Some exception"))
    assert len(exceptions) == 1
    assert exceptions[0] is not None
