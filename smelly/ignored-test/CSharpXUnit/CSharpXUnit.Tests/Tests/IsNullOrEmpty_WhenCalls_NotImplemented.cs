using Xunit;

namespace DefaultNamespace
{
    public class EnvTests
    {
        [Fact(Skip = "")]
        public void IsNullOrEmpty_WhenCalls_NotImplemented()
        {
            {
                throw new NotImplementedException();
            }
        }
    }
}
