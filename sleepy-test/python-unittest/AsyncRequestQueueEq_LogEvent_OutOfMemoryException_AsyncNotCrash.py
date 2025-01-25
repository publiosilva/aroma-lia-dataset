import unittest
import time

class TestAsyncRequestQueueEq(unittest.TestCase):
    def test_log_event_out_of_memory_exception_async_not_crash(self):
        with NoThrowNLogExceptions():
            log_factory = (
                LogFactory()
                .setup()
                .load_configuration(
                    lambda builder: builder.for_logger().write_to(NoMemoryTarget(name="LowMem")).with_async()
                )
                .log_factory
            )
            logger = log_factory.get_current_class_logger()
            target = log_factory.configuration.find_target_by_name("LowMem")

            logger.info("Testing1")
            log_factory.flush()
            self.assertEqual(1, target.failed_count)

            logger.info("Testing2")
            for _ in range(5000):
                if target.write_count != 1:
                    break
                time.sleep(0.001)

            self.assertEqual(2, target.failed_count)

            target.low_memory = False
            logger.info("Testing3")
            for _ in range(5000):
                if target.write_count != 2:
                    break
                time.sleep(0.001)

            self.assertEqual(2, target.failed_count)
            self.assertEqual(3, target.write_count)
