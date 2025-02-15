public class TestCoordActionInputCheckXCommand : XDataTestCase
{
    [Fact]
    public void TestActionInputCheckLatestActionCreationTimeWithPushDependency()
    {
        Services.Get().GetConf().SetBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, false);

        string jobId = "0000000-" + DateTime.UtcNow.Ticks + "-TestCoordActionInputCheckXCommand-C";
        DateTime startTime = DateUtils.ParseDateOozieTZ("2009-02-15T23:59" + TZ);
        DateTime endTime = DateUtils.ParseDateOozieTZ("2009-02-16T23:59" + TZ);
        CoordinatorJobBean job = AddRecordToCoordJobTable(jobId, startTime, endTime, "latest");
        new CoordMaterializeTransitionXCommand(job.Id, 3600).Call();

        // Set push missing dependency
        JPAService jpaService = Services.Get().Get<JPAService>();
        CoordinatorActionBean action = jpaService
                .Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        string pushMissingDependency = "file://" + GetTestCaseDir() + "/2009/02/05";
        action.PushMissingDependencies = pushMissingDependency;
        jpaService.Execute(new CoordActionUpdatePushInputCheckJPAExecutor(action));

        // Update action creation time
        string actionXML = action.ActionXml;
        string actionCreationTime = "2009-02-15T01:00" + TZ;
        actionXML = Regex.Replace(actionXML, "action-actual-time=\".*\">", "action-actual-time=\"" + actionCreationTime + "\">");
        action.ActionXml = actionXML;
        action.CreatedTime = DateUtils.ParseDateOozieTZ(actionCreationTime);
        jpaService.Execute(new CoordActionUpdateForInputCheckJPAExecutor(action));
        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.True(action.ActionXml.Contains("action-actual-time=\"2009-02-15T01:00"));

        new CoordActionInputCheckXCommand(job.Id + "@1", job.Id).Call();
        new CoordPushDependencyCheckXCommand(job.Id + "@1").Call();
        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.Equal(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
                action.MissingDependencies);
        Assert.Equal(pushMissingDependency, action.PushMissingDependencies);

        // providing some of the dataset dirs required as per coordinator specification with holes
        // before and after action creation time
        CreateDir(GetTestCaseDir() + "/2009/03/05/");
        CreateDir(GetTestCaseDir() + "/2009/02/19/");
        CreateDir(GetTestCaseDir() + "/2009/02/12/");
        CreateDir(GetTestCaseDir() + "/2009/01/22/");
        CreateDir(GetTestCaseDir() + "/2009/01/08/");
        CreateDir(GetTestCaseDir() + "/2009/01/01/");

        // Run input check after making latest available
        new CoordActionInputCheckXCommand(job.Id + "@1", job.Id).Call();
        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.Equal(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
                action.MissingDependencies);
        Assert.Equal(pushMissingDependency, action.PushMissingDependencies);

        // Run input check after making push dependencies available
        CreateDir(GetTestCaseDir() + "/2009/02/05");
        new CoordPushDependencyCheckXCommand(job.Id + "@1").Call();
        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.Equal("", action.PushMissingDependencies);
        CheckCoordAction(job.Id + "@1", CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR
                + "${coord:latestRange(-3,0)}", CoordinatorAction.Status.WAITING);
        new CoordActionInputCheckXCommand(job.Id + "@1", job.Id).Call();
        // Sleep for some time as it gets requeued with 10ms delay on failure to acquire write lock
        // Thread.Sleep(1000);

        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.Equal("", action.MissingDependencies);
        actionXML = action.ActionXml;
        // Datasets only before action creation/actual time should be picked up.
        string resolvedList = "file://" + GetTestCaseDir() + "/2009/02/12" + CoordELFunctions.INSTANCE_SEPARATOR
                + "file://" + GetTestCaseDir() + "/2009/02/05" + CoordELFunctions.INSTANCE_SEPARATOR
                + "file://" + GetTestCaseDir() + "/2009/01/22" + CoordELFunctions.INSTANCE_SEPARATOR
                + "file://" + GetTestCaseDir() + "/2009/01/08";
        Console.WriteLine("Expected: " + resolvedList);
        Console.WriteLine("Actual: " + actionXML.Substring(actionXML.IndexOf("<uris>") + 6, actionXML.IndexOf("</uris>") - actionXML.IndexOf("<uris>") - 6));
        Assert.Equal(resolvedList, actionXML.Substring(actionXML.IndexOf("<uris>") + 6, actionXML.IndexOf("</uris>") - actionXML.IndexOf("<uris>") - 6));
    }
}
