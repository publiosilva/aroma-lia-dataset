public class TestCoordPushDependencyCheckXCommand {
    @Test
    public void testTimeOutWithException1() throws Exception {
        // Test timeout when missing dependencies are from a non existing table
        String newHCatDependency1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil";
        String newHCatDependency2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa";
        String newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;

        String actionId = addInitRecords(newHCatDependency);
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        try {
            new CoordPushDependencyCheckXCommand(actionId, true).call();
            fail();
        }
        catch (Exception e) {
            assertTrue(e.getMessage().contains("NoSuchObjectException"));
        }
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        PartitionDependencyManagerService pdms = Services.get().get(PartitionDependencyManagerService.class);
        HCatAccessorService hcatService = Services.get().get(HCatAccessorService.class);
        assertNull(pdms.getWaitingActions(new HCatURI(newHCatDependency1)));
        assertFalse(hcatService.isRegisteredForNotification(new HCatURI(newHCatDependency1)));

        // Timeout is 10 mins. Change action created time to before 12 min to make the action
        // timeout.
        long timeOutCreationTime = System.currentTimeMillis() - (12 * 60 * 1000);
        setCoordActionCreationTime(actionId, timeOutCreationTime);
        try {
            new CoordPushDependencyCheckXCommand(actionId).call();
            fail();
        }
        catch (Exception e) {
            assertTrue(e.getMessage().contains("NoSuchObjectException"));
        }
        Thread.sleep(100);
        // Check for timeout status and unregistered missing dependencies
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.TIMEDOUT);
    }
}
