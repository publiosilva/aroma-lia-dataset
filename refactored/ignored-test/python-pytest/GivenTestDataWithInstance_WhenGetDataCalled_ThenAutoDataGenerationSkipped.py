import pytest

class TestAutoDataAdapterAttribute:

    # @pytest.mark.skip(reason="")
    def test_given_test_data_with_instance_when_get_data_called_then_auto_data_generation_skipped(self):
        # Arrange
        fixture = Fixture()
        attribute = AutoDataAdapterAttribute(fixture, SpecificTestClass.Create())
        method_info = TestAutoDataAdapterAttribute.test_method_with_abstract_test_class.__code__
        # Act
        data = attribute.get_data(method_info)
        # Assert
        assert len(data) == 1
        assert len(data[0]) == method_info.co_argcount
        assert all(d is not None for d in data[0])
        assert all(d == data[0][-1] for d in data[1:])
