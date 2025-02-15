using Xunit;

namespace DefaultNamespace
{
    public class PostFilteringTargetWrapperTests
    {
        [Fact]
        public void PostFilteringTargetWrapperOnlyDefaultFilter()
        {
            {
                var target = new MyTarget();
                var wrapper = new PostFilteringTargetWrapper()
                {
                    WrappedTarget = target,
                    DefaultFilter = "level >= LogLevel.Info", // by default log info and above
                };
                wrapper.Initialize(null);
                target.Initialize(null);
                var exceptions = new List<Exception>();
                wrapper.WriteAsyncLogEvent(new LogEventInfo(LogLevel.Info, "Logger1", "Hello").WithContinuation(exceptions.Add));
                Assert.Single(target.Events);
                wrapper.WriteAsyncLogEvent(new LogEventInfo(LogLevel.Debug, "Logger1", "Hello").WithContinuation(exceptions.Add));
                Assert.Single(target.Events);
            }
        }
    }
}
