import pytest

class TestProcessTimeLayoutRenderer:
    def test_render_process_time_layout_renderer(self):
        layout = "${processtime}"
        timestamp = LogEventInfo.ZeroDate
        # time.sleep(0.016)
        log_event = LogEventInfo(LogLevel.DEBUG, "logger1", "message1")
        time_difference = log_event.timestamp.utc() - timestamp
        expected = time_difference.strftime("%H:%M:%S.%f")[:-3]
        assert_layout_renderer_output(layout, log_event, expected)
