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
        assertNull(result);
        assertNull(result5);
        assertEquals("", layout.render(LogEventInfo.createNullEvent()));
        assertTrue(layout.isFixed());
        assertNull(layout.getFixedValue());
        assertEquals("null", layout.toString());
        assertEquals(nullValue, layout);
        assertNotEquals(0, layout);
    }
}
