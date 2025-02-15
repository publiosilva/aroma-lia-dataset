import pytest

@pytest.mark.skip(reason="")
def test_access_validation_when_calls_not_implemented():
    raise NotImplementedError()
