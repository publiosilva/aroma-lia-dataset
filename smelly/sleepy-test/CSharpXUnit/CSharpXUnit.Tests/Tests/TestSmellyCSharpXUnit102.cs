public class TestCoordPushDependencyCheckXCommand : XDataTestCase
{
    [Fact]
    public void TestTimeOut()
    {
        string db = "default";
        string table = "tablename";
        string newHCatDependency1 = "hcat://" + server + "/" + db + "/" + table + "/dt=20120430;country=brazil";
        string newHCatDependency2 = "hcat://" + server + "/" + db + "/" + table + "/dt=20120430;country=usa";
        string newHCatDependency = newHCatDependency1 + CoordELFunctions.INSTANCE_SEPARATOR + newHCatDependency2;
        PopulateTable(db, table);

        string actionId = AddInitRecords(newHCatDependency);
        CheckCoordAction(actionId, newHCatDependency, CoordinatorAction.Status.WAITING);
        new CoordPushDependencyCheckXCommand(actionId, true).Call();
        CheckCoordAction(actionId, newHCatDependency1, CoordinatorAction.Status.WAITING);
        PartitionDependencyManagerService pdms = Services.Get().Get<PartitionDependencyManagerService>();
        HCatAccessorService hcatService = Services.Get().Get<HCatAccessorService>();
        Assert.True(pdms.GetWaitingActions(new HCatURI(newHCatDependency1)).Contains(actionId));
        Assert.True(hcatService.IsRegisteredForNotification(new HCatURI(newHCatDependency1)));

        long timeOutCreationTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - (12 * 60 * 1000);
        SetCoordActionCreationTime(actionId, timeOutCreationTime);
        new CoordPushDependencyCheckXCommand(actionId).Call();
        Thread.Sleep(100);
        CheckCoordAction(actionId, newHCatDependency1, CoordinatorAction.Status.TIMEDOUT);
        Assert.Null(pdms.GetWaitingActions(new HCatURI(newHCatDependency1)));
        Assert.False(hcatService.IsRegisteredForNotification(new HCatURI(newHCatDependency1)));
    }
}
