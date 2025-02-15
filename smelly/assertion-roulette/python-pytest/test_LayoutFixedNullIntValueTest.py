import pytest

class TestLayoutTyped:
    def test_layout_fixed_null_int_value(self):
        # Arrange
        null_value = None
        layout = Layout(null_value)
        # Act
        result = layout.render_value(LogEventInfo.create_null_event())
        result5 = layout.render_value(LogEventInfo.create_null_event(), 5)
        # Assert
        assert result is None
        assert result5 is None
        assert layout.render(LogEventInfo.create_null_event()) == ""
        assert layout.is_fixed
        assert layout.fixed_value is None
        assert str(layout) == "null"
        assert layout == null_value
        assert layout != 0
