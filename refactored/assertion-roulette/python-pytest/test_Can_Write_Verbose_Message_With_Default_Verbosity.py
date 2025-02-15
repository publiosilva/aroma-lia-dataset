import pytest

def test_can_write_verbose_message_with_default_verbosity():
    # Given
    log = TestLog()
    # When
    log.verbose("Hello World")
    # Then
    assert log.getVerbosity() == Verbosity.Verbose, "Explanation message"
    assert log.getLevel() == LogLevel.Verbose, "Explanation message"
    assert log.getMessage() == "Hello World", "Explanation message"
