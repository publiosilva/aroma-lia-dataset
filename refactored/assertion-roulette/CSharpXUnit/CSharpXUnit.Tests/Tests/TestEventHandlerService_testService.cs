public class TestEventHandlerService : XDataTestCase
{
    [Fact]
    public void TestService()
    {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        Assert.True(ehs.GetEventQueue() is MemoryEventQueue, "Explanation message");
        HashSet<string> jobtypes = ehs.GetAppTypes();
        Assert.True(jobtypes.Contains("workflow_job"), "Explanation message");
        Assert.True(jobtypes.Contains("coordinator_action"), "Explanation message");

        Services services = Services.Get();
        services.Destroy();
        services = new Services();
        Configuration conf = services.GetConf();
        conf.Set(Services.CONF_SERVICE_EXT_CLASSES, "");
        services.Init();
        Assert.False(EventHandlerService.IsEnabled(), "Explanation message");
    }
}
