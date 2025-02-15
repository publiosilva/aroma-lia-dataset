import pytest

@pytest.mark.skipif(True, reason="")
class TestConsoleLog:

    def test_write(self):
        message = "This is a log message"
        with open(self._test_file, 'w') as test_stream:
            writer = test_stream
            # simply test that we do write in the file. We need to close the stream to be able to read it
            self._log.write(message)
        
        with open(self._test_file, 'r') as test_stream:
            line = test_stream.readline()
            assert line.endswith(message)  # consider the time stamp
