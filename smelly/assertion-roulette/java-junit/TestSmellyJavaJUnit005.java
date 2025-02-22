import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ComponentRenderingTest {

    @Test
    public void canUseAddMultipleAttributes() {
        {
            var cut = renderComponent(DuplicateAttributesComponent.class);
            var element = cut.find("#duplicate-on-element > div");
            assertTrue(element.hasAttribute("bool")); // attribute is present
            assertEquals("middle-value", element.getAttribute("string"));
            assertEquals("unmatched-value", element.getAttribute("unmatched"));
            element = cut.find("#duplicate-on-element-override > div");
            assertFalse(element.hasAttribute("bool")); // attribute is not present
            assertEquals("other-text", element.getAttribute("string"));
            assertEquals("unmatched-value", element.getAttribute("unmatched"));
        }
    }
}
