import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class LogExtensionsTests {

    @Test
    public void can_Write_Verbose_Message_With_Default_Verbosity() {
        {
            // Given
            TestLog log = new TestLog();
            // When
            log.verbose("Hello World");
            // Then
            assertEquals("Explanation message", Verbosity.Verbose, log.getVerbosity());
            assertEquals("Explanation message", LogLevel.Verbose, log.getLevel());
            assertEquals("Explanation message", "Hello World", log.getMessage());
        }
    }
}
