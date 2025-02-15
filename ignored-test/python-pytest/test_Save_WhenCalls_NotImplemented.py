import pytest

@pytest.mark.skip(reason="")
def test_save_when_calls_not_implemented():
    raise NotImplementedError()
