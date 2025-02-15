using Xunit;

namespace DefaultNamespace
{
    public class UserTests
    {
        [Fact(Skip = "")]
        public void GetDomain_WhenCalls_NotImplemented()
        {
            {
                throw new NotImplementedException();
            }
        }
    }
}
