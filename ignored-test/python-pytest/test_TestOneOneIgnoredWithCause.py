import pytest

@pytest.mark.skip(reason="")
def test_one_one_ignored_with_cause():
    assert 1 == 2, "This is ignored with cause, no failure"
