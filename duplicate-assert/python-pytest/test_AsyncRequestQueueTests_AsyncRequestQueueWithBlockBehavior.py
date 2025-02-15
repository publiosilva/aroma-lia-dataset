import pytest
import threading
import time

class TestAsyncRequestQueue:
    
    def test_async_request_queue_with_block_behavior(self):
        queue = AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block)
        producer_finished = threading.Event()
        pushing_event = 0

        def producer():
            nonlocal pushing_event
            for i in range(1000):
                log_event = LogEventInfo.create_null_event().with_continuation(lambda ex: None)
                log_event.log_event.message = f"msg{i}"
                pushing_event = i
                queue.enqueue(log_event)
            producer_finished.set()

        threading.Thread(target=producer).start()
        total = 0
        
        while total < 500:
            left = 500 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            assert got <= queue.request_limit
            total += got

        time.sleep(0.5)
        assert pushing_event == 510
        queue.dequeue_batch(1)
        total += 1
        time.sleep(0.5)
        assert pushing_event == 511
        
        while total < 1000:
            left = 1000 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            assert got <= queue.request_limit
            total += got

        producer_finished.wait()
