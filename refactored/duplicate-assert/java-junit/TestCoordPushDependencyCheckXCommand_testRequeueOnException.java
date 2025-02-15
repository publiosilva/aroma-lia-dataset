public class TestCoordPushDependencyCheckXCommand extends TestCase {
    @Test
    public void testRequeueOnException1() throws Exception {
        Services.get().getConf().setInt(RecoveryService.CONF_SERVICE_INTERVAL, 6000);
        // Test timeout when table containing missing dependencies is dropped in between
        String newHCatDependency1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil";
        String newHCatDependency2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa";
        String newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;


        CoordinatorJobBean job = addRecordToCoordJobTableForWaiting("coord-job-for-action-input-check.xml",
                CoordinatorJob.Status.RUNNING, false, true);

        CoordinatorActionBean action = addRecordToCoordActionTableForWaiting(job.getId(), 1,
                CoordinatorAction.Status.WAITING, "coord-action-for-action-input-check.xml", null,
                newHCatDependency, "Z");
        String actionId = action.getId();
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        try {
            new CoordPushDependencyCheckXCommand(actionId, true).call();
            fail();
        }
        catch (Exception e) {
            assertTrue(e.getMessage().contains("NoSuchObjectException"));
        }
        // Nothing should be queued as there are no pull dependencies
        CallableQueueService callableQueueService = Services.get().get(CallableQueueService.class);
        assertEquals(0, callableQueueService.getQueueDump().size());

        // Nothing should be queued as there are no pull missing dependencies
        // but only push missing deps are there
        new CoordActionInputCheckXCommand(actionId, job.getId()).call();
        callableQueueService = Services.get().get(CallableQueueService.class);

        setMissingDependencies(actionId, newHCatDependency1);
        try {
            new CoordPushDependencyCheckXCommand(actionId, true).call();
            fail();
        }
        catch (Exception e) {
            assertTrue(e.getMessage().contains("NoSuchObjectException"));
        }
        // Should be requeued at the recovery service interval
        final List<String> queueDump = callableQueueService.getQueueDump();
        assertEquals(1, callableQueueService.getQueueDump().size());
        assertTrue(queueDump.get(0).contains(CoordPushDependencyCheckXCommand.class.getName()));
        log.info("Queue dump is " + queueDump.toString());
        // Delay should be something like delay=599999. Ignore last three digits
        assertTrue(queueDump.get(0).matches("delay=599[0-9]{3}, .*"));
    }

    @Test
    public void testRequeueOnException2() throws Exception {
        Services.get().getConf().setInt(RecoveryService.CONF_SERVICE_INTERVAL, 6000);
        // Test timeout when table containing missing dependencies is dropped in between
        String newHCatDependency1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil";
        String newHCatDependency2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa";
        String newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;


        CoordinatorJobBean job = addRecordToCoordJobTableForWaiting("coord-job-for-action-input-check.xml",
                CoordinatorJob.Status.RUNNING, false, true);

        CoordinatorActionBean action = addRecordToCoordActionTableForWaiting(job.getId(), 1,
                CoordinatorAction.Status.WAITING, "coord-action-for-action-input-check.xml", null,
                newHCatDependency, "Z");
        String actionId = action.getId();
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        try {
            new CoordPushDependencyCheckXCommand(actionId, true).call();
            fail();
        }
        catch (Exception e) {
            assertTrue(e.getMessage().contains("NoSuchObjectException"));
        }
        // Nothing should be queued as there are no pull dependencies
        CallableQueueService callableQueueService = Services.get().get(CallableQueueService.class);

        // Nothing should be queued as there are no pull missing dependencies
        // but only push missing deps are there
        new CoordActionInputCheckXCommand(actionId, job.getId()).call();
        callableQueueService = Services.get().get(CallableQueueService.class);
        assertEquals(0, callableQueueService.getQueueDump().size());

        setMissingDependencies(actionId, newHCatDependency1);
        try {
            new CoordPushDependencyCheckXCommand(actionId, true).call();
            fail();
        }
        catch (Exception e) {
            assertTrue(e.getMessage().contains("NoSuchObjectException"));
        }
        // Should be requeued at the recovery service interval
        final List<String> queueDump = callableQueueService.getQueueDump();
        assertEquals(1, callableQueueService.getQueueDump().size());
        assertTrue(queueDump.get(0).contains(CoordPushDependencyCheckXCommand.class.getName()));
        log.info("Queue dump is " + queueDump.toString());
        // Delay should be something like delay=599999. Ignore last three digits
        assertTrue(queueDump.get(0).matches("delay=599[0-9]{3}, .*"));
    }
}
