import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;

import static org.junit.jupiter.api.Assertions.assertTrue;

public class ClassOne {

    @Test
    @Disabled("")
    public void testOneOneIgnored() {
        assertTrue(1 == 2, "This is ignored, no failure");
    }
}
