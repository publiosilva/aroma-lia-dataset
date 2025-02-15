import pytest

class TestCoordPushDependencyCheckXCommand:
    def test_requeue_on_exception_1(self):
        Services.get().getConf().setInt(RecoveryService.CONF_SERVICE_INTERVAL, 6000)
        new_hcat_dependency1 = f"hcat://{server}/nodb/notable/dt=20120430;country=brazil"
        new_hcat_dependency2 = f"hcat://{server}/nodb/notable/dt=20120430;country=usa"
        new_hcat_dependency = f"{new_hcat_dependency1}{CoordELFunctions.INSTANCE_SEPARATOR}{new_hcat_dependency2}"

        job = add_record_to_coord_job_table_for_waiting("coord-job-for-action-input-check.xml",
                CoordinatorJob.Status.RUNNING, False, True)

        action = add_record_to_coord_action_table_for_waiting(job.getId(), 1,
                CoordinatorAction.Status.WAITING, "coord-action-for-action-input-check.xml", None,
                new_hcat_dependency, "Z")
        action_id = action.getId()
        check_coord_action(action_id, new_hcat_dependency, CoordinatorAction.Status.WAITING)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(action_id, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)
        callable_queue_service = Services.get().get(CallableQueueService.class)
        assert len(callable_queue_service.getQueueDump()) == 0

        CoordActionInputCheckXCommand(action_id, job.getId()).call()
        callable_queue_service = Services.get().get(CallableQueueService.class)

        set_missing_dependencies(action_id, new_hcat_dependency1)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(action_id, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)

        queue_dump = callable_queue_service.getQueueDump()
        assert len(queue_dump) == 1
        assert CoordPushDependencyCheckXCommand.class.getName() in queue_dump[0]
        log.info(f"Queue dump is {queue_dump}")
        assert re.match(r"delay=599[0-9]{3}, .*", queue_dump[0])

    def test_requeue_on_exception_2(self):
        Services.get().getConf().setInt(RecoveryService.CONF_SERVICE_INTERVAL, 6000)
        new_hcat_dependency1 = f"hcat://{server}/nodb/notable/dt=20120430;country=brazil"
        new_hcat_dependency2 = f"hcat://{server}/nodb/notable/dt=20120430;country=usa"
        new_hcat_dependency = f"{new_hcat_dependency1}{CoordELFunctions.INSTANCE_SEPARATOR}{new_hcat_dependency2}"

        job = add_record_to_coord_job_table_for_waiting("coord-job-for-action-input-check.xml",
                CoordinatorJob.Status.RUNNING, False, True)

        action = add_record_to_coord_action_table_for_waiting(job.getId(), 1,
                CoordinatorAction.Status.WAITING, "coord-action-for-action-input-check.xml", None,
                new_hcat_dependency, "Z")
        action_id = action.getId()
        check_coord_action(action_id, new_hcat_dependency, CoordinatorAction.Status.WAITING)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(action_id, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)
        
        callable_queue_service = Services.get().get(CallableQueueService.class)

        CoordActionInputCheckXCommand(action_id, job.getId()).call()
        callable_queue_service = Services.get().get(CallableQueueService.class)
        assert len(callable_queue_service.getQueueDump()) == 0

        set_missing_dependencies(action_id, new_hcat_dependency1)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(action_id, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)

        queue_dump = callable_queue_service.getQueueDump()
        assert len(queue_dump) == 1
        assert CoordPushDependencyCheckXCommand.class.getName() in queue_dump[0]
        log.info(f"Queue dump is {queue_dump}")
        assert re.match(r"delay=599[0-9]{3}, .*", queue_dump[0])
