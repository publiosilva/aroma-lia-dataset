import pytest

class TestPollsChoiceModelImpl:
    @pytest.mark.timeout(4000)
    def test_00(self):
        polls_choice_impl_0 = PollsChoiceImpl()
        polls_choice_wrapper_0 = PollsChoiceWrapper(polls_choice_impl_0)
        polls_choice_wrapper_0.set_choice_id(1)
        boolean_0 = polls_choice_impl_0.equals(polls_choice_wrapper_0)
        assert polls_choice_impl_0.get_choice_id() == 1, "Explanation message"
        assert boolean_0, "Explanation message"
