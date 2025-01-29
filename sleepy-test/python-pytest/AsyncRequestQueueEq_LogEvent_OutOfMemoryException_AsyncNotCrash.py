def test_log_event_out_of_memory_exception_async_not_crash():
    with NoThrowNLogExceptions():
        log_factory = LogFactory().Setup().LoadConfiguration(lambda builder: builder.ForLogger().WriteTo(NoMemoryTarget(Name="LowMem")).WithAsync()).LogFactory
        logger = log_factory.GetCurrentClassLogger()
        target = log_factory.Configuration.FindTargetByName("LowMem")
        logger.Info("Testing1")
        log_factory.Flush()
        assert target.FailedCount == 1
        logger.Info("Testing2")
        for _ in range(5000):
            if target.WriteCount != 1:
                break
            time.sleep(0.001)
        assert target.FailedCount == 2
        target.LowMemory = False
        logger.Info("Testing3")
        for _ in range(5000):
            if target.WriteCount != 2:
                break
            time.sleep(0.001)
        assert target.FailedCount == 2
        assert target.WriteCount == 3
