import pytest

# @pytest.mark.skip
class TestUser:

    def test_get_domain_when_calls_not_implemented(self):
        raise NotImplementedError()
