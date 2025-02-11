using Xunit;

namespace None
{
    public class MyFluentWrapper
    {
        [Fact(Skip = "")]
        public void CallSiteShouldWorkForAsyncMethodsWithReturnValue()
        {
            {
                var callSite = GetAsyncCallSite().GetAwaiter().GetResult();
                Assert.Equal("NLog.UnitTests.LayoutRenderers.CallSiteTests.GetAsyncCallSite", callSite);
            }
        }
    }
}
