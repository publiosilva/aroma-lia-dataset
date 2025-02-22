import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.condition.DisabledIf;

import java.io.*;

import static org.junit.jupiter.api.Assertions.assertTrue;

class ConsoleLogTest {

    @Test
    @Ignore
    public void testWrite() throws IOException {
        String message = "This is a log message";
        try (FileOutputStream testStream = new FileOutputStream(_testFile);
             PrintWriter writer = new PrintWriter(testStream)) {
            System.setOut(writer);
            _log.writeLine(message);
        }

        try (FileInputStream testStream = new FileInputStream(_testFile);
             BufferedReader reader = new BufferedReader(new InputStreamReader(testStream))) {
            String line = reader.readLine();
            assertTrue(line.endsWith(message)); // consider the time stamp
        }
    }
}
