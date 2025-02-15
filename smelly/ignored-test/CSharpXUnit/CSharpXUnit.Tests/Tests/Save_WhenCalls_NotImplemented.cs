using Xunit;

namespace DefaultNamespace
{
    public class LogTests
    {
        [Fact(Skip = "")]
        public void Save_WhenCalls_NotImplemented()
        {
            {
                throw new NotImplementedException();
            }
        }
    }
}
