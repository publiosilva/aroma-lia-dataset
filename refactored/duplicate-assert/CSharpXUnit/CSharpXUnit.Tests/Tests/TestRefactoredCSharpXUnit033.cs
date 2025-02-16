using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

public class AsyncHelperTests
{
    [Fact]
    public void continuationTimeoutNotHitTest1()
    {
        {
            List<Exception> exceptions = new List<Exception>();
            var cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.Add), 50);
            cont(null);
            Thread.Sleep(100);
            Assert.Equal(1, exceptions.Count);
            Assert.Null(exceptions[0]);
        }
    }

    [Fact]
    public void continuationTimeoutNotHitTest2()
    {
        {
            List<Exception> exceptions = new List<Exception>();
            var cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions.Add), 50);
            cont(null);
            Thread.Sleep(100);
            cont(null);
            cont(new ApplicationException("Some exception"));
            cont(null);
            cont(new ApplicationException("Some exception"));
            Assert.Equal(1, exceptions.Count);
            Assert.Null(exceptions[0]);
        }
    }
}
