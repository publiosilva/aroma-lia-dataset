@pytest.mark.parametrize('pollsChoiceImpl', [PollsChoiceImpl()])
def test_00(pollsChoiceImpl):
    pollsChoiceWrapper = PollsChoiceWrapper(pollsChoiceImpl)
    pollsChoiceWrapper.set_choice_id(1)
    boolean_value = pollsChoiceImpl == pollsChoiceWrapper
    assert pollsChoiceImpl.get_choice_id() == 1
    assert boolean_value
