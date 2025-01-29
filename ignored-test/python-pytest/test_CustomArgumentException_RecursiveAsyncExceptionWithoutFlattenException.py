import pytest

class TestCustomArgumentException:

    @pytest.mark.skip(reason="")
    def test_recursive_async_exception_without_flatten_exception(self):
        recursion_count = 3
        def inner_action():
            raise ApplicationException("Life is hard")
        t1 = System.Threading.Tasks.Task[int].factory.start_new(
            lambda: NestedFunc(recursion_count, inner_action)
        )
        try:
            t1.wait()
        except AggregateException as ex:
            layout_renderer = ExceptionLayoutRenderer()
            layout_renderer.format = "ToString"
            layout_renderer.flatten_exception = False
            log_event = LogEventInfo.create(LogLevel.error, None, None, ex)
            result = layout_renderer.render(log_event)
            needle_count = 0
            found_index = result.find("NestedFunc", 0)
            while found_index >= 0:
                needle_count += 1
                found_index = result.find("NestedFunc", found_index + len("NestedFunc"))
            assert needle_count >= recursion_count, f"{needle_count} too small"
