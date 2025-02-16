import pytest

class TestComponentRendering:

    def test_can_use_add_multiple_attributes(self):
        cut = render_component(DuplicateAttributesComponent)
        element = cut.find("#duplicate-on-element > div")
        assert element.has_attribute("bool"), "Explanation message"  # attribute is present
        assert element.get_attribute("string") == "middle-value", "Explanation message"
        assert element.get_attribute("unmatched") == "unmatched-value", "Explanation message"
        element = cut.find("#duplicate-on-element-override > div")
        assert not element.has_attribute("bool"), "Explanation message"  # attribute is not present
        assert element.get_attribute("string") == "other-text", "Explanation message"
        assert element.get_attribute("unmatched") == "unmatched-value", "Explanation message"
