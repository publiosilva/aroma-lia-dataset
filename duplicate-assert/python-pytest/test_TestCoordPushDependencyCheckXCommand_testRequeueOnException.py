import pytest

class TestCoordPushDependencyCheckXCommand:

    def test_requeue_on_exception(self):
        Services.get().getConf().setInt(RecoveryService.CONF_SERVICE_INTERVAL, 6000)
        newHCatDependency1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil"
        newHCatDependency2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa"
        newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2

        job = addRecordToCoordJobTableForWaiting("coord-job-for-action-input-check.xml",
                CoordinatorJob.Status.RUNNING, False, True)

        action = addRecordToCoordActionTableForWaiting(job.getId(), 1,
                CoordinatorAction.Status.WAITING, "coord-action-for-action-input-check.xml", None,
                newHCatDependency, "Z")
        actionId = action.getId()
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(actionId, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)

        callableQueueService = Services.get().get(CallableQueueService.class)
        assert len(callableQueueService.getQueueDump()) == 0

        CoordActionInputCheckXCommand(actionId, job.getId()).call()
        callableQueueService = Services.get().get(CallableQueueService.class)
        assert len(callableQueueService.getQueueDump()) == 0

        setMissingDependencies(actionId, newHCatDependency1)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(actionId, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)

        queueDump = callableQueueService.getQueueDump()
        assert len(queueDump) == 1
        assert CoordPushDependencyCheckXCommand.__name__ in queueDump[0]
        log.info("Queue dump is " + str(queueDump))
        assert re.match(r"delay=599[0-9]{3}, .*", queueDump[0])
