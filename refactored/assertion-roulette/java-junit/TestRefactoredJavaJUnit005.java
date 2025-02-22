import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ComponentRenderingTest {

    @Test
    public void canUseAddMultipleAttributes() {
        {
            var cut = renderComponent(DuplicateAttributesComponent.class);
            var element = cut.find("#duplicate-on-element > div");
            assertTrue("Explanation message", element.hasAttribute("bool")); // attribute is present
            assertEquals("Explanation message", "middle-value", element.getAttribute("string"));
            assertEquals("Explanation message", "unmatched-value", element.getAttribute("unmatched"));
            element = cut.find("#duplicate-on-element-override > div");
            assertFalse("Explanation message", element.hasAttribute("bool")); // attribute is not present
            assertEquals("Explanation message", "other-text", element.getAttribute("string"));
            assertEquals("Explanation message", "unmatched-value", element.getAttribute("unmatched"));
        }
    }
}
