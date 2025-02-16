import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class LayoutTypedTests {

    @Test
    void layoutFixedNullIntValueTest() {
        // Arrange
        Integer nullValue = null;
        Layout<Integer> layout = new Layout<>(nullValue);
        // Act
        var result = layout.renderValue(LogEventInfo.createNullEvent());
        var result5 = layout.renderValue(LogEventInfo.createNullEvent(), 5);
        // Assert
        assertNull("Explanation message", result);
        assertNull("Explanation message", result5);
        assertEquals("Explanation message", "", layout.render(LogEventInfo.createNullEvent()));
        assertTrue("Explanation message", layout.isFixed());
        assertNull("Explanation message", layout.getFixedValue());
        assertEquals("Explanation message", "null", layout.toString());
        assertEquals("Explanation message", nullValue, layout);
        assertNotEquals("Explanation message", 0, layout);
    }
}
