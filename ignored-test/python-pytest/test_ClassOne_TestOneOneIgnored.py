import pytest

class TestClassOne:

    @pytest.mark.skip(reason="")
    def test_one_one_ignored(self):
        assert 1 == 2, "This is ignored, no failure"
