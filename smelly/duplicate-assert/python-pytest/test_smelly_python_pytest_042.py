import pytest

class TestPostFilteringTargetWrapper:
    
    def test_post_filtering_target_wrapper_only_default_filter(self):
        target = MyTarget()
        wrapper = PostFilteringTargetWrapper()
        wrapper.WrappedTarget = target
        wrapper.DefaultFilter = "level >= LogLevel.Info"  # by default log info and above
        wrapper.Initialize(None)
        target.Initialize(None)
        exceptions = []
        wrapper.WriteAsyncLogEvent(LogEventInfo(LogLevel.Info, "Logger1", "Hello").WithContinuation(exceptions.append))
        assert len(target.Events) == 1
        wrapper.WriteAsyncLogEvent(LogEventInfo(LogLevel.Debug, "Logger1", "Hello").WithContinuation(exceptions.append))
        assert len(target.Events) == 1
