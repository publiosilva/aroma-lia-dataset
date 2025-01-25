import unittest
import time

class TestAsyncTaskBatchExceptionTarget(unittest.TestCase):
    def test_async_task_target_test_throttle_on_task_delay(self):
        async_target = AsyncTaskBatchTestTarget(
            layout="${level}",
            task_delay_milliseconds=50,
            batch_size=10,
        )
        logger = (
            LogFactory()
            .setup()
            .load_configuration(
                lambda builder: builder.for_logger().write_to(async_target)
            )
            .get_current_class_logger()
        )

        for i in range(5):
            for j in range(10):
                logger.log(LogLevel.INFO, str(i))
                time.sleep(0.02)

            self.assertTrue(async_target.wait_for_write_event())

        self.assertTrue(
            len(async_target.logs) > 25,
            f"{len(async_target.logs)} LogEvents are too few after {async_target.write_tasks} writes",
        )
        self.assertTrue(
            async_target.write_tasks < 20,
            f"{async_target.write_tasks} writes are too many.",
        )
