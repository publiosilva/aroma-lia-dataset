import pytest

class TestLog:
    
    # @pytest.mark.skip
    def test_access_validation_when_calls_not_implemented(self):
        raise NotImplementedError()
