import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class FileLengthCheckTest extends AbstractModuleTestSupport {

    @Test
    public void TestExtensions1() {
        FileLengthCheck check = new FileLengthCheck();
        check.setFileExtensions("java");
        assertEquals(".java", check.getFileExtensions()[0], "extension should be the same");

        Exception exception = assertThrows(IllegalArgumentException.class, () -> check.setFileExtensions(null));
        assertEquals("Extensions array can not be null", exception.getMessage(), "Invalid exception message");
    }

    @Test
    public void TestExtensions2() {
        FileLengthCheck check = new FileLengthCheck();
        check.setFileExtensions(".java");
        assertEquals(".java", check.getFileExtensions()[0], "extension should be the same");

        Exception exception = assertThrows(IllegalArgumentException.class, () -> check.setFileExtensions(null));
        assertEquals("Extensions array can not be null", exception.getMessage(), "Invalid exception message");
    }
}
