public class TestCoordActionInputCheckXCommand : XDataTestCase
{
    [Fact]
    public void TestActionInputCheckLatestActionCreationTime()
    {
        Services.Get().GetConf().SetBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, false);

        string jobId = "0000000-" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + "-TestCoordActionInputCheckXCommand-C";
        DateTime startTime = DateUtils.ParseDateOozieTZ("2009-02-15T23:59" + TZ);
        DateTime endTime = DateUtils.ParseDateOozieTZ("2009-02-16T23:59" + TZ);
        CoordinatorJobBean job = AddRecordToCoordJobTable(jobId, startTime, endTime, "latest");
        new CoordMaterializeTransitionXCommand(job.Id, 3600).Call();

        JPAService jpaService = Services.Get().Get<JPAService>();
        CoordinatorActionBean action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.Equal(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
            action.MissingDependencies);

        // Update action creation time
        string actionXML = action.ActionXml;
        string actionCreationTime = "2009-02-15T01:00" + TZ;
        actionXML = Regex.Replace(actionXML, "action-actual-time=\".*\">", "action-actual-time=\"" + actionCreationTime + "\">");
        action.ActionXml = actionXML;
        action.CreatedTime = DateUtils.ParseDateOozieTZ(actionCreationTime);
        jpaService.Execute(new CoordActionUpdateForInputCheckJPAExecutor(action));
        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.True(action.ActionXml.Contains("action-actual-time=\"2009-02-15T01:00"));

        // providing some of the dataset dirs required as per coordinator specification with holes
        // before and after action creation time
        CreateDir(GetTestCaseDir() + "/2009/03/05/");
        CreateDir(GetTestCaseDir() + "/2009/02/19/");
        CreateDir(GetTestCaseDir() + "/2009/02/12/");
        CreateDir(GetTestCaseDir() + "/2009/02/05/");
        CreateDir(GetTestCaseDir() + "/2009/01/22/");
        CreateDir(GetTestCaseDir() + "/2009/01/08/");

        new CoordActionInputCheckXCommand(job.Id + "@1", job.Id).Call();
        //Sleep for sometime as it gets requeued with 10ms delay on failure to acquire write lock
        // Thread.Sleep(1000);

        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        actionXML = action.ActionXml;
        Assert.Equal("", action.MissingDependencies);
        // Datasets only before action creation/actual time should be picked up.
        string resolvedList = "file://" + GetTestCaseDir() + "/2009/02/12" + CoordELFunctions.INSTANCE_SEPARATOR
            + "file://" + GetTestCaseDir() + "/2009/02/05" + CoordELFunctions.INSTANCE_SEPARATOR
            + "file://" + GetTestCaseDir() + "/2009/01/22" + CoordELFunctions.INSTANCE_SEPARATOR
            + "file://" + GetTestCaseDir() + "/2009/01/08";
        Console.WriteLine("Expected: " + resolvedList);
        Console.WriteLine("Actual: " + actionXML.Substring(actionXML.IndexOf("<uris>") + 6, actionXML.IndexOf("</uris>") - (actionXML.IndexOf("<uris>") + 6)));
        Assert.Equal(resolvedList, actionXML.Substring(actionXML.IndexOf("<uris>") + 6, actionXML.IndexOf("</uris>") - (actionXML.IndexOf("<uris>") + 6)));
    }
}
