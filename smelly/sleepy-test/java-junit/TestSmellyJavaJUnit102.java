public class TestCoordPushDependencyCheckXCommand {
    @Test
    public void testTimeOut() throws Exception {
        String db = "default";
        String table = "tablename";
        String newHCatDependency1 = "hcat://" + server + "/" + db + "/" + table + "/dt=20120430;country=brazil";
        String newHCatDependency2 = "hcat://" + server + "/" + db + "/" + table + "/dt=20120430;country=usa";
        String newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;
        populateTable(db, table);

        String actionId = addInitRecords(newHCatDependency);
        checkCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        new CoordPushDependencyCheckXCommand(actionId, true).call();
        checkCoordAction(actionId, newHCatDependency1, CoordinatorAction.Status.WAITING);
        PartitionDependencyManagerService pdms = Services.get().get(PartitionDependencyManagerService.class);
        HCatAccessorService hcatService = Services.get().get(HCatAccessorService.class);
        assertTrue(pdms.getWaitingActions(new HCatURI(newHCatDependency1)).contains(actionId));
        assertTrue(hcatService.isRegisteredForNotification(new HCatURI(newHCatDependency1)));

        // Timeout is 10 mins. Change action created time to before 12 min to make the action
        // timeout.
        long timeOutCreationTime = System.currentTimeMillis() - (12 * 60 * 1000);
        setCoordActionCreationTime(actionId, timeOutCreationTime);
        new CoordPushDependencyCheckXCommand(actionId).call();
        Thread.sleep(100);
        // Check for timeout status and unregistered missing dependencies
        checkCoordAction(actionId, newHCatDependency1, CoordinatorAction.Status.TIMEDOUT);
        assertNull(pdms.getWaitingActions(new HCatURI(newHCatDependency1)));
        assertFalse(hcatService.isRegisteredForNotification(new HCatURI(newHCatDependency1)));

    }
}
