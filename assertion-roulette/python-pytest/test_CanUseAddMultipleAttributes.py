import pytest

class TestComponentRendering:
    def test_can_use_add_multiple_attributes(self):
        cut = render_component(DuplicateAttributesComponent)
        element = cut.find("#duplicate-on-element > div")
        assert element.has_attribute("bool")  # attribute is present
        assert element.get_attribute("string") == "middle-value"
        assert element.get_attribute("unmatched") == "unmatched-value"
        element = cut.find("#duplicate-on-element-override > div")
        assert not element.has_attribute("bool")  # attribute is not present
        assert element.get_attribute("string") == "other-text"
        assert element.get_attribute("unmatched") == "unmatched-value"
