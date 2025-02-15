public class TestCoordActionInputCheckXCommand extends XDataTestCase {
    public void testActionInputCheckLatestActionCreationTime() throws Exception {
        Services.get().getConf().setBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, false);

        String jobId = "0000000-" + new Date().getTime() + "-TestCoordActionInputCheckXCommand-C";
        Date startTime = DateUtils.parseDateOozieTZ("2009-02-15T23:59" + TZ);
        Date endTime = DateUtils.parseDateOozieTZ("2009-02-16T23:59" + TZ);
        CoordinatorJobBean job = addRecordToCoordJobTable(jobId, startTime, endTime, "latest");
        new CoordMaterializeTransitionXCommand(job.getId(), 3600).call();

        JPAService jpaService = Services.get().get(JPAService.class);
        CoordinatorActionBean action = jpaService
                .execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        assertEquals(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
                action.getMissingDependencies());

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

        // providing some of the dataset dirs required as per coordinator specification with holes
        // before and after action creation time
        createDir(getTestCaseDir() + "/2009/03/05/");
        createDir(getTestCaseDir() + "/2009/02/19/");
        createDir(getTestCaseDir() + "/2009/02/12/");
        createDir(getTestCaseDir() + "/2009/02/05/");
        createDir(getTestCaseDir() + "/2009/01/22/");
        createDir(getTestCaseDir() + "/2009/01/08/");

        new CoordActionInputCheckXCommand(job.getId() + "@1", job.getId()).call();
        //Sleep for sometime as it gets requeued with 10ms delay on failure to acquire write lock
        Thread.sleep(1000);

        action = jpaService.execute(new CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"));
        actionXML = action.getActionXml();
        assertEquals("", action.getMissingDependencies());
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
