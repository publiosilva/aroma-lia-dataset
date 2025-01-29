import pytest

class TestClassOne:

    @pytest.mark.skip(reason="This is the ignore cause")
    def test_one_one_ignored_with_cause(self):
        assert 1 == 2, "This is ignored with cause, no failure"
