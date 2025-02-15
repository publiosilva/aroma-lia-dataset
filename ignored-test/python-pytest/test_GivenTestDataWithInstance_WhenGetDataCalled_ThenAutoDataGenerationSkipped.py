import pytest

@pytest.mark.skip(reason="")
class TestAutoDataAdapterAttribute:

    def test_given_test_data_with_instance_when_get_data_called_then_auto_data_generation_skipped(self):
        # Arrange
        fixture = Fixture()
        attribute = AutoDataAdapterAttribute(fixture, SpecificTestClass.create())
        method_info = TestAutoDataAdapterAttribute.test_method_with_abstract_test_class

        # Act
        data = list(attribute.get_data(method_info))

        # Assert
        assert len(data) == 1
        assert len(data[0]) == len(method_info.__code__.co_varnames)
        assert all(x is not None for x in data[0])
        assert all(x == data[0][-1] for x in data[0][1:])
