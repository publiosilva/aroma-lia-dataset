using Xunit;

namespace DefaultNamespace
{
    public class AspMemoryCacheTests
    {
        [Fact]
        public void should_clear_key_if_ttl_expired()
        {
            {
                var fake = new Fake(1);
                _cache.Add("1", fake, TimeSpan.FromMilliseconds(50), "region");
                Thread.Sleep(200);
                var result = _cache.Get("1", "region");
                result.ShouldBeNull();
            }
        }
    }
}
