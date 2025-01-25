import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;

import static org.junit.jupiter.api.Assertions.assertTrue;

public class ClassOne {

    @Test
    @Disabled("This is the ignore cause")
    public void testOneOneIgnoredWithCause() {
        assertTrue(1 == 2, "This is ignored with cause, no failure");
    }
}
