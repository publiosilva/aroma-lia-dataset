import pytest
import time

class TestExample3D:
    @pytest.mark.parametrize("param", [1])  # Added to mimic xUnit structure
    def test_long(self, param):
        print("Example3B: TestLong started")
        # time.sleep(6)
