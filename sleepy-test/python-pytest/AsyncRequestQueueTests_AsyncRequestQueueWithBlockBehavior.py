def test_async_request_queue_with_block_behavior():
    queue = AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block)
    producer_finished = threading.Event()
    pushing_event = [0]

    def producer_thread():
        for i in range(1000):
            log_event = LogEventInfo.CreateNullEvent().WithContinuation(lambda ex: None)
            log_event.LogEvent.Message = f"msg{i}"
            pushing_event[0] = i
            queue.Enqueue(log_event)
        producer_finished.set()

    threading.Thread(target=producer_thread).start()

    total = 0
    while total < 500:
        left = 500 - total
        log_event_infos = queue.DequeueBatch(left)
        got = len(log_event_infos)
        assert got <= queue.RequestLimit
        total += got

    time.sleep(0.5)
    assert pushing_event[0] == 510
    queue.DequeueBatch(1)
    total += 1
    time.sleep(0.5)
    assert pushing_event[0] == 511

    while total < 1000:
        left = 1000 - total
        log_event_infos = queue.DequeueBatch(left)
        got = len(log_event_infos)
        assert got <= queue.RequestLimit
        total += got

    producer_finished.wait()
