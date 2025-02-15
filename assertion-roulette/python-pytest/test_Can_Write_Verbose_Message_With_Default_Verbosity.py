import pytest

class TestLog:
    # Assume the TestLog class and its functionality are implemented here

def test_can_write_verbose_message_with_default_verbosity():
    # Given
    log = TestLog()
    # When
    log.verbose("Hello World")
    # Then
    assert log.verbosity == Verbosity.Verbose
    assert log.level == LogLevel.Verbose
    assert log.message == "Hello World"
