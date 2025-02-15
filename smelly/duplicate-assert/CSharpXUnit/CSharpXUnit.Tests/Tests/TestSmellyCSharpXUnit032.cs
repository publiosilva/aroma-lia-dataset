using Xunit;

namespace nlog
{
    public class AsyncHelperTests
    {
        [Fact]
        public void ContinuationErrorTimeoutNotHitTest()
        {
            {
                var exceptions = new List<Exception>();
                // set up a timer to strike
                var cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.Add), TimeSpan.FromMilliseconds(50));
                var exception = new ApplicationException("Foo");
                // call success quickly, hopefully before the timer comes
                cont(exception);
                // sleep to make sure timer event comes
                Thread.Sleep(100);
                // make sure we got success, not a timer exception
                Assert.Single(exceptions);
                Assert.NotNull(exceptions[0]);
                Assert.Same(exception, exceptions[0]);
                // those will be ignored
                cont(null);
                cont(new ApplicationException("Some exception"));
                cont(null);
                cont(new ApplicationException("Some exception"));
                Assert.Single(exceptions);
                Assert.NotNull(exceptions[0]);
            }
        }
    }
}
