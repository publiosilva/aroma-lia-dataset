import pytest

class TestIExistsForTestingValueRetrieving:
    
    def test_should_allow_the_removal_and_addition_of_new_value_retrievers(self):
        service = Service()
        for value_retriever in service.value_retrievers[:]:
            service.value_retrievers.unregister(value_retriever)
            assert value_retriever not in service.value_retrievers
        
        thing = TestIExistsForTestingValueRetrieving()
        service.value_retrievers.register(thing)
        assert len(service.value_retrievers) == 1
        assert thing is service.value_retrievers[0]
