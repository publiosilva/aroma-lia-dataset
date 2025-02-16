using Xunit;

public class AutoMapperConfigurationTests {

    [Fact] // [Fact(Skip = "")]
    public void configuration_IsValid() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new DefaultProfile()));
        config.AssertConfigurationIsValid();
    }
}
