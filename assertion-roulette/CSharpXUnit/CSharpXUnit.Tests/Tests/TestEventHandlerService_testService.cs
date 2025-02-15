public class TestEventHandlerService : XDataTestCase
{
    [Fact]
    public void TestService()
    {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        Assert.IsType<MemoryEventQueue>(ehs.GetEventQueue());
        var jobtypes = ehs.GetAppTypes();
        Assert.Contains("workflow_job", jobtypes);
        Assert.Contains("coordinator_action", jobtypes);

        Services services = Services.Get();
        services.Destroy();
        services = new Services();
        Configuration conf = services.GetConf();
        conf.Set(Services.CONF_SERVICE_EXT_CLASSES, "");
        services.Init();
        Assert.False(EventHandlerService.IsEnabled());
    }
}
