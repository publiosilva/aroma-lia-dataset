import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class LayoutTypedTests {
    @Test
    void layoutNotEqualsNullIntValueFixedTest() {
        // Arrange
        Integer nullInt = null;
        Layout<Integer> layout1 = new Layout<>("2");
        Layout<Integer> layout2 = nullInt;
        // Act + Assert
        assertFalse(layout1 == nullInt);
        assertFalse(layout1.equals(nullInt));
        assertFalse(layout1.equals((Object) nullInt));
        assertNotEquals(layout1, layout2);
        assertNotEquals(layout1.hashCode(), layout2.hashCode());
    }
}
