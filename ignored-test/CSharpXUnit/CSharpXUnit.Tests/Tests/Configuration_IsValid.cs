using Xunit;

namespace DefaultNamespace
{
    public class AutoMapperConfigurationTests
    {
        [Fact(Skip = "")]
        public void Configuration_IsValid()
        {
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile(new DefaultProfile()));
                config.AssertConfigurationIsValid();
            }
        }
    }
}
