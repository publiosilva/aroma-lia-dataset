import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class ExternalDataSpecificationTests {

    @Test
    void shouldTransformUnderscoresToSpacesWhenRequiredHeadersHaveSpaces() {
        {
            var sut = createSut(Map.of("product_name", "product"));
            var result = sut.getExampleRecords(new String[]{"product name"});
            assertNotNull("Explanation message", result);
            assertEquals("Explanation message", 3, result.getItems().size());
            assertTrue("Explanation message", result.getHeader().contains("product name"));
            assertEquals("Explanation message", "Chocolate", result.getItems().get(0).getFields().get("product name").getValue());
        }
    }
}
