public class TestUUIDService
{
    [Fact]
    public void TestChildId1()
    {
        SetSystemProperty(UUIDService.CONF_GENERATOR, "counter");
        var services = new Services();
        services.Init();
        try
        {
            var uuid = services.Get<UUIDService>();
            string id = uuid.GenerateId(ApplicationType.WORKFLOW);
            string childId = uuid.GenerateChildId(id, "a");
            Assert.Equal(id, uuid.GetId(childId));
            Assert.Equal("a", uuid.GetChildName(childId));
        }
        finally
        {
            services.Destroy();
        }
    }

    [Fact]
    public void TestChildId2()
    {
        SetSystemProperty(UUIDService.CONF_GENERATOR, "counter");
        var services = new Services();
        services.Init();
        try
        {
            var uuid = services.Get<UUIDService>();
            string id = uuid.GenerateId(ApplicationType.WORKFLOW);
            string childId = uuid.GenerateChildId(id, "a");
        }
        finally
        {
            services.Destroy();
        }

        SetSystemProperty(UUIDService.CONF_GENERATOR, "random");
        services = new Services();
        services.Init();
        try
        {
            var uuid = services.Get<UUIDService>();
            string id = uuid.GenerateId(ApplicationType.WORKFLOW);
            string childId = uuid.GenerateChildId(id, "a");
            Assert.Equal(id, uuid.GetId(childId));
            Assert.Equal("a", uuid.GetChildName(childId));
        }
        finally
        {
            services.Destroy();
        }
    }
}
