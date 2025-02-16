import pytest

@pytest.mark.skip(reason="")
class TestPaths:
    def test_get_directory_name_when_calls_not_implemented(self):
        raise NotImplementedError()
