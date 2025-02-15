import pytest

class TestPostFilteringTargetWrapper:

    def test_post_filtering_target_wrapper_only_default_filter1(self):
        target = MyTarget()
        wrapper = PostFilteringTargetWrapper()
        wrapper.set_wrapped_target(target)
        wrapper.set_default_filter("level >= LogLevel.Info")  # by default log info and above
        wrapper.initialize(None)
        target.initialize(None)
        exceptions = []
        wrapper.write_async_log_event(LogEventInfo(LogLevel.Info, "Logger1", "Hello").with_continuation(exceptions.append))
        assert len(target.get_events()) == 1

    def test_post_filtering_target_wrapper_only_default_filter2(self):
        target = MyTarget()
        wrapper = PostFilteringTargetWrapper()
        wrapper.set_wrapped_target(target)
        wrapper.set_default_filter("level >= LogLevel.Info")  # by default log info and above
        wrapper.initialize(None)
        target.initialize(None)
        exceptions = []
        wrapper.write_async_log_event(LogEventInfo(LogLevel.Info, "Logger1", "Hello").with_continuation(exceptions.append))
        wrapper.write_async_log_event(LogEventInfo(LogLevel.Debug, "Logger1", "Hello").with_continuation(exceptions.append))
        assert len(target.get_events()) == 1
