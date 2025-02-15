import pytest

class TestEnv:
    @pytest.mark.skip(reason="")
    def test_is_null_or_empty_when_calls_not_implemented(self):
        raise NotImplementedError()
