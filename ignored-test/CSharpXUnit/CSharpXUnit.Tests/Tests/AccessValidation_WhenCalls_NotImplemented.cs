using Xunit;

namespace DefaultNamespace
{
    public class LogTests
    {
        [Fact(Skip = "")]
        public void AccessValidation_WhenCalls_NotImplemented()
        {
            {
                throw new NotImplementedException();
            }
        }
    }
}
