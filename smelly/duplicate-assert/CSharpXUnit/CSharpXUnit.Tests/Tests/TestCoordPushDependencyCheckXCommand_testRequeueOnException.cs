public class TestCoordPushDependencyCheckXCommand : XDataTestCase
{
    [Fact]
    public void TestRequeueOnException()
    {
        Services.Get().GetConf().SetInt(RecoveryService.CONF_SERVICE_INTERVAL, 6000);
        string newHCatDependency1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil";
        string newHCatDependency2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa";
        string newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;

        CoordinatorJobBean job = AddRecordToCoordJobTableForWaiting("coord-job-for-action-input-check.xml",
            CoordinatorJob.Status.RUNNING, false, true);

        CoordinatorActionBean action = AddRecordToCoordActionTableForWaiting(job.Id, 1,
            CoordinatorAction.Status.WAITING, "coord-action-for-action-input-check.xml", null,
            newHCatDependency, "Z");
        string actionId = action.Id;
        CheckCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        try
        {
            new CoordPushDependencyCheckXCommand(actionId, true).Call();
            Assert.True(false);
        }
        catch (Exception e)
        {
            Assert.True(e.Message.Contains("NoSuchObjectException"));
        }
        var callableQueueService = Services.Get().Get<CallableQueueService>();
        Assert.Equal(0, callableQueueService.GetQueueDump().Count);

        new CoordActionInputCheckXCommand(actionId, job.Id).Call();
        callableQueueService = Services.Get().Get<CallableQueueService>();
        Assert.Equal(0, callableQueueService.GetQueueDump().Count);

        SetMissingDependencies(actionId, newHCatDependency1);
        try
        {
            new CoordPushDependencyCheckXCommand(actionId, true).Call();
            Assert.True(false);
        }
        catch (Exception e)
        {
            Assert.True(e.Message.Contains("NoSuchObjectException"));
        }
        var queueDump = callableQueueService.GetQueueDump();
        Assert.Equal(1, queueDump.Count);
        Assert.True(queueDump[0].Contains(typeof(CoordPushDependencyCheckXCommand).FullName));
        log.Info("Queue dump is " + string.Join(", ", queueDump));
        Assert.True(System.Text.RegularExpressions.Regex.IsMatch(queueDump[0], "delay=599[0-9]{3}, .*"));
    }
}
