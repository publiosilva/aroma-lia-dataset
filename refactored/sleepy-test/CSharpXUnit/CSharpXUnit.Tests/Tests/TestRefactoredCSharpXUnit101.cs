public class TestCoordActionInputCheckXCommand : XDataTestCase
{
    [Fact]
    public void TestActionInputCheckLatestCurrentTime()
    {
        Services.Get().GetConf().SetBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, true);

        string jobId = "0000000-" + DateTime.Now.Ticks + "-TestCoordActionInputCheckXCommand-C";
        DateTime startTime = DateUtils.ParseDateOozieTZ("2009-02-15T23:59" + TZ);
        DateTime endTime = DateUtils.ParseDateOozieTZ("2009-02-16T23:59" + TZ);
        CoordinatorJobBean job = AddRecordToCoordJobTable(jobId, startTime, endTime, "latest");
        new CoordMaterializeTransitionXCommand(job.Id, 3600).Call();

        JPAService jpaService = Services.Get().Get<JPAService>();
        CoordinatorActionBean action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.Equal(CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}",
            action.MissingDependencies);

        string actionXML = action.ActionXml;
        string actionCreationTime = "2009-02-15T01:00" + TZ;
        actionXML = Regex.Replace(actionXML, "action-actual-time=\".*\">", "action-actual-time=\"" + actionCreationTime + "\">");
        action.ActionXml = actionXML;
        action.CreatedTime = DateUtils.ParseDateOozieTZ(actionCreationTime);
        jpaService.Execute(new CoordActionUpdateForInputCheckJPAExecutor(action));
        action = jpaService.Execute(new CoordActionGetForInputCheckJPAExecutor(job.Id + "@1"));
        Assert.True(action.ActionXml.Contains("action-actual-time=\"2009-02-15T01:00"));

        CreateDir(GetTestCaseDir() + "/2009/03/05/");
        CreateDir(GetTestCaseDir() + "/2009/02/19/");
        CreateDir(GetTestCaseDir() + "/2009/02/12/");
        CreateDir(GetTestCaseDir() + "/2009/02/05/");
        CreateDir(GetTestCaseDir() + "/2009/01/22/");
        CreateDir(GetTestCaseDir() + "/2009/01/08/");

        new CoordActionInputCheckXCommand(job.Id + "@1", job.Id).Call();
        // Thread.Sleep(1000);

        action = jpaService.Execute(new CoordActionGetJPAExecutor(job.Id + "@1"));
        actionXML = action.ActionXml;
        Assert.Equal("", action.MissingDependencies);
        string resolvedList = "file://" + GetTestCaseDir() + "/2009/03/05" + CoordELFunctions.INSTANCE_SEPARATOR
            + "file://" + GetTestCaseDir() + "/2009/02/19" + CoordELFunctions.INSTANCE_SEPARATOR
            + "file://" + GetTestCaseDir() + "/2009/02/12" + CoordELFunctions.INSTANCE_SEPARATOR
            + "file://" + GetTestCaseDir() + "/2009/02/05";
        Assert.Equal(resolvedList, actionXML.Substring(actionXML.IndexOf("<uris>") + 6, actionXML.IndexOf("</uris>") - (actionXML.IndexOf("<uris>") + 6)));
    }
}
