import pytest
import time

class TestClassStp:
    @pytest.mark.parametrize("test_input,expected", [(1, 1)])
    def test_two_two_long(self, test_input, expected):
        time.sleep(0.541)
        assert test_input == expected
