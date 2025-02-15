public class TestCoordActionInputCheckXCommand extends TestCase {
    public void testActionInputCheckLatestActionCreationTimeWithPushDependency() throws Exception {
        Services.get().getConf().setBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, false);

        String jobId = "0000000-" + new Date().getTime() + "-TestCoordActionInputCheckXCommand-C";
        Date startTime = DateUtils.parseDateOozieTZ("2009-02-15T23:59" + TZ);
        Date endTime = DateUtils.parseDateOozieTZ("2009-02-16T23:59" + TZ);
        CoordinatorJobBean job = addRecordToCoordJobTable(jobId, startTime, endTime, "latest");
        new CoordMaterializeTransitionXCommand(job.getId(), 3600).call();

        // Set push missing dependency
        JPAService jpaService = Services.get().get(JPAService.class);
        CoordinatorActionBean action = jpaService
                .execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        final String pushMissingDependency = "file://" + getTestCaseDir() + "/2009/02/05";
        action.setPushMissingDependencies(pushMissingDependency);
        jpaService.execute(new CoordActionUpdatePushInputCheckJPAExecutor(action));

        // Update action creation time
        String actionXML = action.getActionXml();
        String actionCreationTime = "2009-02-15T01:00" + TZ;
        actionXML = actionXML.replaceAll("action-actual-time=\".*\">", "action-actual-time=\"" + actionCreationTime
                + "\">");
        action.setActionXml(actionXML);
        action.setCreatedTime(DateUtils.parseDateOozieTZ(actionCreationTime));
        jpaService.execute(new CoordActionUpdateForInputCheckJPAExecutor(action));
        action = jpaService.execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        assertTrue(action.getActionXml().contains("action-actual-time=\"2009-02-15T01:00")) ;

        new CoordActionInputCheckXCommand(job.getId() + "@1", job.getId()).call();
        new CoordPushDependencyCheckXCommand(job.getId() + "@1").call();
        action = jpaService.execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        assertEquals(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
                action.getMissingDependencies());
        assertEquals(pushMissingDependency, action.getPushMissingDependencies());

        // providing some of the dataset dirs required as per coordinator specification with holes
        // before and after action creation time
        createDir(getTestCaseDir() + "/2009/03/05/");
        createDir(getTestCaseDir() + "/2009/02/19/");
        createDir(getTestCaseDir() + "/2009/02/12/");
        createDir(getTestCaseDir() + "/2009/01/22/");
        createDir(getTestCaseDir() + "/2009/01/08/");
        createDir(getTestCaseDir() + "/2009/01/01/");

        // Run input check after making latest available
        new CoordActionInputCheckXCommand(job.getId() + "@1", job.getId()).call();
        action = jpaService.execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        assertEquals(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
                action.getMissingDependencies());
        assertEquals(pushMissingDependency, action.getPushMissingDependencies());

        // Run input check after making push dependencies available
        createDir(getTestCaseDir() + "/2009/02/05");
        new CoordPushDependencyCheckXCommand(job.getId() + "@1").call();
        action = jpaService.execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        assertEquals("", action.getPushMissingDependencies());
        checkCoordAction(job.getId() + "@1", CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR
                + "${coord:latestRange(-3,0)}", CoordinatorAction.Status.WAITING);
        new CoordActionInputCheckXCommand(job.getId() + "@1", job.getId()).call();
        //Sleep for sometime as it gets requeued with 10ms delay on failure to acquire write lock
        // Thread.sleep(1000);

        action = jpaService.execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        assertEquals("", action.getMissingDependencies());
        actionXML = action.getActionXml();
        // Datasets only before action creation/actual time should be picked up.
        String resolvedList = "file://" + getTestCaseDir() + "/2009/02/12" + CoordELFunctions.INSTANCE_SEPARATOR
                + "file://" + getTestCaseDir() + "/2009/02/05" + CoordELFunctions.INSTANCE_SEPARATOR
                + "file://" + getTestCaseDir() + "/2009/01/22" + CoordELFunctions.INSTANCE_SEPARATOR
                + "file://" + getTestCaseDir() + "/2009/01/08";
        System.out.println("Expected: " + resolvedList);
        System.out.println("Actual: " +  actionXML.substring(actionXML.indexOf("<uris>") + 6, actionXML.indexOf("</uris>")));
        assertEquals(resolvedList, actionXML.substring(actionXML.indexOf("<uris>") + 6, actionXML.indexOf("</uris>")));
    }
}
