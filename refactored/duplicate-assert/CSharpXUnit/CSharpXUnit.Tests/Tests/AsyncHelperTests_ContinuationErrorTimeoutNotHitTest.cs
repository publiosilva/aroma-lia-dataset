using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

public class AsyncHelperTests
{
    [Fact]
    public void ContinuationErrorTimeoutNotHitTest1()
    {
        {
            List<Exception> exceptions = new List<Exception>();
            var cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.Add), 50);
            Exception exception = new ApplicationException("Foo");
            cont.Apply(exception);
            Thread.Sleep(100);
            Assert.Equal(1, exceptions.Count);
            Assert.NotNull(exceptions[0]);
            Assert.Same(exception, exceptions[0]);
        }
    }

    [Fact]
    public void ContinuationErrorTimeoutNotHitTest2()
    {
        {
            List<Exception> exceptions = new List<Exception>();
            var cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.Add), 50);
            Exception exception = new ApplicationException("Foo");
            cont.Apply(exception);
            Thread.Sleep(100);
            cont.Apply(null);
            cont.Apply(new ApplicationException("Some exception"));
            cont.Apply(null);
            cont.Apply(new ApplicationException("Some exception"));
            Assert.Equal(1, exceptions.Count);
            Assert.NotNull(exceptions[0]);
        }
    }
}
