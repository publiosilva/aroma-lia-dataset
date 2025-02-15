import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class ExternalDataSpecificationTests {

    @Test
    void shouldTransformUnderscoresToSpacesWhenRequiredHeadersHaveSpaces() {
        {
            var sut = createSut(Map.of("product_name", "product"));
            var result = sut.getExampleRecords(new String[]{"product name"});
            assertNotNull(result);
            assertEquals(3, result.getItems().size());
            assertTrue(result.getHeader().contains("product name"));
            assertEquals("Chocolate", result.getItems().get(0).getFields().get("product name").getValue());
        }
    }
}
