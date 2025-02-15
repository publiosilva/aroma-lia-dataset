import pytest

class TestExternalDataSpecification:

    def test_should_transform_underscores_to_spaces_when_required_headers_have_spaces(self):
        sut = create_sut({"product_name": "product"})
        result = sut.get_example_records(["product name"])
        assert result is not None, "Explanation message"
        assert len(result.get_items()) == 3, "Explanation message"
        assert "product name" in result.get_header(), "Explanation message"
        assert result.get_items()[0].get_fields()["product name"].get_value() == "Chocolate", "Explanation message"
