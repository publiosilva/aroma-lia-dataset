import pytest

class TestAsyncRequestQueueEq:

    def test_log_event_out_of_memory_exception_async_not_crash1(self):
        with NoThrowNLogExceptions():
            log_factory = LogFactory().setup().load_configuration(lambda builder: builder.for_logger().write_to(NoMemoryTarget(name="LowMem")).with_async()).get_log_factory()
            logger = log_factory.get_current_class_logger()
            target = log_factory.get_configuration().find_target_by_name("LowMem")
            logger.info("Testing1")
            log_factory.flush()
            assert target.get_failed_count() == 1
            logger.info("Testing2")
            for i in range(5000):
                if target.get_write_count() != 1:
                    break
                time.sleep(1)

            assert target.get_failed_count() == 2

    def test_log_event_out_of_memory_exception_async_not_crash2(self):
        with NoThrowNLogExceptions():
            log_factory = LogFactory().setup().load_configuration(lambda builder: builder.for_logger().write_to(NoMemoryTarget(name="LowMem")).with_async()).get_log_factory()
            logger = log_factory.get_current_class_logger()
            target = log_factory.get_configuration().find_target_by_name("LowMem")
            logger.info("Testing1")
            log_factory.flush()
            assert target.get_failed_count() == 1
            logger.info("Testing2")
            for i in range(5000):
                if target.get_write_count() != 1:
                    break
                time.sleep(1)

            target.set_low_memory(False)
            logger.info("Testing3")
            for i in range(5000):
                if target.get_write_count() != 2:
                    break
                time.sleep(1)

            assert target.get_failed_count() == 2
            assert target.get_write_count() == 3
