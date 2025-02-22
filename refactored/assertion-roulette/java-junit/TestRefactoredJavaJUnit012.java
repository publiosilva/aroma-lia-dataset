import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class LayoutTypedTests {
    @Test
    public void layoutNotEqualsNullIntValueFixedTest() {
        // Arrange
        Integer nullInt = null;
        Layout<Integer> layout1 = new Layout<>("2");
        Layout<Integer> layout2 = nullInt;
        // Act + Assert
        assertFalse("Explanation message", layout1 == nullInt);
        assertFalse("Explanation message", layout1.equals(nullInt));
        assertFalse("Explanation message", layout1.equals((Object) nullInt));
        assertNotEquals("Explanation message", layout1, layout2);
        assertNotEquals("Explanation message", layout1.hashCode(), layout2.hashCode());
    }
}
