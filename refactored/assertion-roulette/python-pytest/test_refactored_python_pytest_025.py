import pytest

class TestArgumentTests:

    def test_time_span_argument_has_default(self):
        arg = SampleTimeSpanArgument(Duration(minutes=3))
        command = UnitTestCommand.from_argument(arg)
        exit_code = command.invoke([])
        assert exit_code == 0, "Explanation message"
        assert command.is_command_run(), "Explanation message"
        assert arg.get_value() == Duration(minutes=3), "Explanation message"
