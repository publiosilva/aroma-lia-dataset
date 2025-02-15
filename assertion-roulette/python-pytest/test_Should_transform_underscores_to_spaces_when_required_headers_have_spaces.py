import pytest

class TestExternalDataSpecification:
    def test_should_transform_underscores_to_spaces_when_required_headers_have_spaces(self):
        sut = create_sut({"product_name": "product"})
        result = sut.get_example_records(["product name"])
        assert result is not None
        assert len(result.items) == 3
        assert "product name" in result.header
        assert result.items[0].fields["product name"].value == "Chocolate"
