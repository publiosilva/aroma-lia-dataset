using Xunit;

public class PostFilteringTargetWrapperTests
{
    [Fact]
    public void PostFilteringTargetWrapperOnlyDefaultFilter1()
    {
        var target = new MyTarget();
        var wrapper = new PostFilteringTargetWrapper();
        wrapper.SetWrappedTarget(target);
        wrapper.SetDefaultFilter("level >= LogLevel.Info"); // by default log info and above
        wrapper.Initialize(null);
        target.Initialize(null);
        var exceptions = new List<Exception>();
        wrapper.WriteAsyncLogEvent(new LogEventInfo(LogLevel.Info, "Logger1", "Hello").WithContinuation(exceptions.Add));
        Assert.Equal(1, target.GetEvents().Count);
    }

    [Fact]
    public void PostFilteringTargetWrapperOnlyDefaultFilter2()
    {
        var target = new MyTarget();
        var wrapper = new PostFilteringTargetWrapper();
        wrapper.SetWrappedTarget(target);
        wrapper.SetDefaultFilter("level >= LogLevel.Info"); // by default log info and above
        wrapper.Initialize(null);
        target.Initialize(null);
        var exceptions = new List<Exception>();
        wrapper.WriteAsyncLogEvent(new LogEventInfo(LogLevel.Info, "Logger1", "Hello").WithContinuation(exceptions.Add));
        wrapper.WriteAsyncLogEvent(new LogEventInfo(LogLevel.Debug, "Logger1", "Hello").WithContinuation(exceptions.Add));
        Assert.Equal(1, target.GetEvents().Count);
    }
}
