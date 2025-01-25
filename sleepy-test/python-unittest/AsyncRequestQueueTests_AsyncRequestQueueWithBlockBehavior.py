import unittest
import threading
import time

class TestAsyncRequestQueue(unittest.TestCase):
    def test_async_request_queue_with_block_behavior(self):
        queue = AsyncRequestQueue(10, AsyncTargetWrapperOverflowAction.BLOCK)
        producer_finished = threading.Event()
        pushing_event = [0]

        def producer_thread():
            for i in range(1000):
                log_event = LogEventInfo.create_null_event().with_continuation(lambda ex: None)
                log_event.log_event.message = f"msg{i}"
                pushing_event[0] = i
                queue.enqueue(log_event)
            producer_finished.set()

        threading.Thread(target=producer_thread).start()

        total = 0
        while total < 500:
            left = 500 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            self.assertTrue(got <= queue.request_limit)
            total += got

        time.sleep(0.5)
        self.assertEqual(510, pushing_event[0])

        queue.dequeue_batch(1)
        total += 1
        time.sleep(0.5)
        self.assertEqual(511, pushing_event[0])

        while total < 1000:
            left = 1000 - total
            log_event_infos = queue.dequeue_batch(left)
            got = len(log_event_infos)
            self.assertTrue(got <= queue.request_limit)
            total += got

        producer_finished.wait()
