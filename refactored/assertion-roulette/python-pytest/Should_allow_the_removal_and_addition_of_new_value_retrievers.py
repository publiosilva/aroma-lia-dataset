import pytest

class TestIExistsForTestingValueRetrieving:

    @pytest.mark.parametrize("value_retriever", [])
    def test_should_allow_removal_and_addition_of_new_value_retrievers(self, value_retriever):
        service = Service()
        for value_retriever in list(service.get_value_retrievers()):
            service.get_value_retrievers().unregister(value_retriever)
            assert not service.get_value_retrievers().contains(value_retriever), "Explanation message"

        thing = IExistsForTestingValueRetrieving()
        service.get_value_retrievers().register(thing)
        assert service.get_value_retrievers().size() == 1, "Explanation message"
        assert service.get_value_retrievers().get(0) is thing, "Explanation message"
