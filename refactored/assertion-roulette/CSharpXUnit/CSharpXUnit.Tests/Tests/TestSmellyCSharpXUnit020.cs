public class TestEventHandlerService : XDataTestCase
{
    [Fact]
    public void TestService1()
    {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        Assert.IsType<MemoryEventQueue>(ehs.GetEventQueue());
    }

    [Fact]
    public void TestService2()
    {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        var jobtypes = ehs.GetAppTypes();
        Assert.Contains("workflow_job", jobtypes);
    }

    [Fact]
    public void TestService3()
    {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        var jobtypes = ehs.GetAppTypes();
        Assert.Contains("coordinator_action", jobtypes);
    }

    [Fact]
    public void TestService4()
    {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        var jobtypes = ehs.GetAppTypes();

        Services services = Services.Get();
        services.Destroy();
        services = new Services();
        Configuration conf = services.GetConf();
        conf.Set(Services.CONF_SERVICE_EXT_CLASSES, "");
        services.Init();
        Assert.False(EventHandlerService.IsEnabled());
    }
}
