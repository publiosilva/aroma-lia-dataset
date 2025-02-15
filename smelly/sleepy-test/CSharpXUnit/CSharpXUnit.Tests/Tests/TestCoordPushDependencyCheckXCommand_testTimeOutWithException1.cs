public class TestCoordPushDependencyCheckXCommand : XDataTestCase
{
    [Fact]
    public void TestTimeOutWithException1()
    {
        // Test timeout when missing dependencies are from a non existing table
        string newHCatDependency1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil";
        string newHCatDependency2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa";
        string newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;

        string actionId = AddInitRecords(newHCatDependency);
        CheckCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        try
        {
            new CoordPushDependencyCheckXCommand(actionId, true).Call();
            Assert.True(false);
        }
        catch (Exception e)
        {
            Assert.Contains("NoSuchObjectException", e.Message);
        }
        CheckCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        PartitionDependencyManagerService pdms = Services.Get<PartitionDependencyManagerService>();
        HCatAccessorService hcatService = Services.Get<HCatAccessorService>();
        Assert.Null(pdms.GetWaitingActions(new HCatURI(newHCatDependency1)));
        Assert.False(hcatService.IsRegisteredForNotification(new HCatURI(newHCatDependency1)));

        // Timeout is 10 mins. Change action created time to before 12 min to make the action
        // timeout.
        long timeOutCreationTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - (12 * 60 * 1000);
        SetCoordActionCreationTime(actionId, timeOutCreationTime);
        try
        {
            new CoordPushDependencyCheckXCommand(actionId).Call();
            Assert.True(false);
        }
        catch (Exception e)
        {
            Assert.Contains("NoSuchObjectException", e.Message);
        }
        Thread.Sleep(100);
        // Check for timeout status and unregistered missing dependencies
        CheckCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.TIMEDOUT);
    }
}
