import pytest
import time
from threading import Thread, Event

class TestAsyncRequestQueue:

    @pytest.mark.asyncio
    async def test_async_request_queue_with_block_behavior_1(self):
        queue = AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block)
        producer_finished = Event()
        pushing_event = [0]

        def producer_thread():
            for i in range(1000):
                log_event = LogEventInfo.create_null_event().with_continuation(lambda ex: None)
                log_event.get_log_event().set_message(f"msg{i}")
                pushing_event[0] = i
                queue.enqueue(log_event)
            producer_finished.set()

        producer = Thread(target=producer_thread)
        producer.start()

        total = 0
        while total < 500:
            left = 500 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            assert got <= queue.get_request_limit()
            total += got

        time.sleep(0.5)
        assert pushing_event[0] == 510
        queue.dequeue_batch(1)
        total += 1
        time.sleep(0.5)
        assert pushing_event[0] == 511

        while total < 1000:
            left = 1000 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            total += got

        producer_finished.wait()

    @pytest.mark.asyncio
    async def test_async_request_queue_with_block_behavior_2(self):
        queue = AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.Block)
        producer_finished = Event()
        pushing_event = [0]

        def producer_thread():
            for i in range(1000):
                log_event = LogEventInfo.create_null_event().with_continuation(lambda ex: None)
                log_event.get_log_event().set_message(f"msg{i}")
                pushing_event[0] = i
                queue.enqueue(log_event)
            producer_finished.set()

        producer = Thread(target=producer_thread)
        producer.start()

        total = 0
        while total < 500:
            left = 500 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            total += got

        time.sleep(0.5)
        assert pushing_event[0] == 510
        queue.dequeue_batch(1)
        total += 1
        time.sleep(0.5)
        assert pushing_event[0] == 511

        while total < 1000:
            left = 1000 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            assert got <= queue.get_request_limit()
            total += got

        producer_finished.wait()
