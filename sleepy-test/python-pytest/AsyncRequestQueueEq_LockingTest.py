def test_locking():
    target = MyTarget()
    target.Initialize(None)
    mre = threading.Event()
    background_thread_exception = None

    def background_thread():
        nonlocal background_thread_exception
        try:
            target.BlockingOperation(500)
        except Exception as ex:
            background_thread_exception = ex
        finally:
            mre.set()

    t = threading.Thread(target=background_thread)
    target.Initialize(None)
    t.start()
    time.sleep(0.05)
    exceptions = []
    target.WriteAsyncLogEvent(LogEventInfo.CreateNullEvent().WithContinuation(exceptions.append))
    target.WriteAsyncLogEvents([
        LogEventInfo.CreateNullEvent().WithContinuation(exceptions.append),
        LogEventInfo.CreateNullEvent().WithContinuation(exceptions.append)
    ])
    target.Flush(exceptions.append)
    target.Close()
    for exception in exceptions:
        assert exception is None
    mre.wait()
    if background_thread_exception is not None:
        assert False, str(background_thread_exception)
