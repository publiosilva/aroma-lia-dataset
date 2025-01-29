def test_async_task_target_test_throttle_on_task_delay():
    async_target = AsyncTaskBatchTestTarget(
        Layout="${level}",
        TaskDelayMilliseconds=50,
        BatchSize=10,
    )
    logger = LogFactory().Setup().LoadConfiguration(
        lambda builder: builder.ForLogger().WriteTo(async_target)
    ).GetCurrentClassLogger()

    for i in range(5):
        for j in range(10):
            logger.Log(LogLevel.Info, str(i))
            time.sleep(0.02)

        assert async_target.WaitForWriteEvent()

    assert len(async_target.Logs) > 25, f"{len(async_target.Logs)} LogEvents are too few after {async_target.WriteTasks} writes"
    assert async_target.WriteTasks < 20, f"{async_target.WriteTasks} writes are too many."
