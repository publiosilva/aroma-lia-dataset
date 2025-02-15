import pytest

@pytest.mark.timeout(4000)
def test03():
    get_revision = GetRevision("no edit sum found found")
    http_action0 = get_revision.get_next_message()
    assert http_action0.get_request() == "/no edit sum found found?format=txt"
    
    http_action1 = get_revision.get_next_message()
    assert http_action1 is not None
    assert http_action1.get_request() == "/no edit sum found found"
    
    boolean0 = get_revision.has_more_messages()
    assert boolean0 is True
