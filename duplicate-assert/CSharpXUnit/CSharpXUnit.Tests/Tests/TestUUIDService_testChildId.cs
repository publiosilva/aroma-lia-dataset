using Xunit;

public class TestUUIDService
{
    [Fact]
    public void TestChildId()
    {
        SetSystemProperty(UUIDService.CONF_GENERATOR, "counter");
        Services services = new Services();
        services.Init();
        try
        {
            UUIDService uuid = services.Get<UUIDService>();
            string id = uuid.GenerateId(ApplicationType.WORKFLOW);
            string childId = uuid.GenerateChildId(id, "a");
            Assert.Equal(id, uuid.GetId(childId));
            Assert.Equal("a", uuid.GetChildName(childId));
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
            UUIDService uuid = services.Get<UUIDService>();
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