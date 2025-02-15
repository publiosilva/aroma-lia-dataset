import pytest

class TestArgument:
    def test_timespan_argument_has_default(self):
        arg = SampleTimeSpanArgument(timedelta(minutes=3))
        command = UnitTestCommand.from_argument(arg)
        exit_code = command.invoke([])
        assert exit_code == 0
        assert command.command_run
        assert arg.value == timedelta(minutes=3)
