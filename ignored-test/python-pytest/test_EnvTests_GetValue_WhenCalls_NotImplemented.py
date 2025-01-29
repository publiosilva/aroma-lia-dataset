import pytest

class TestEnv:

    @pytest.mark.skip(reason="")
    def test_get_value_when_calls_not_implemented(self):
        raise NotImplementedError()
