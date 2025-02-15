import pytest

class TestConsoleLog:

    # @pytest.mark.skipif(True, reason="Condition to skip test")
    def test_write(self):
        message = "This is a log message"
        with open(_test_file, 'w') as test_file:
            print(message, file=test_file)

        with open(_test_file, 'r') as test_file:
            line = test_file.readline()
            assert line.endswith(message)  # consider the time stamp
