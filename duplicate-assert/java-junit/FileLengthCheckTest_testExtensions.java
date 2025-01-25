import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.fail;

import org.junit.jupiter.api.Test;

public class FileLengthCheckTest extends AbstractModuleTestSupport {

    @Test
    public void testExtensions() {
        final FileLengthCheck check = new FileLengthCheck();
        check.setFileExtensions("java");
        assertEquals(".java", check.getFileExtensions()[0], "extension should be the same");
        check.setFileExtensions(".java");
        assertEquals(".java", check.getFileExtensions()[0], "extension should be the same");
        try {
            check.setFileExtensions((String[]) null);
            fail("IllegalArgumentException is expected");
        }
        catch (IllegalArgumentException ex) {
            assertEquals("Extensions array can not be null", ex.getMessage(),
                    "Invalid exception message");
        }
    }
}
