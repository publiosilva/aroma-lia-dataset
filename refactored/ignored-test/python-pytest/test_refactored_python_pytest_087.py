import pytest

# @pytest.mark.skip(reason="This is ignored with cause, no failure")
def test_one_one_ignored_with_cause():
    assert 1 == 2
