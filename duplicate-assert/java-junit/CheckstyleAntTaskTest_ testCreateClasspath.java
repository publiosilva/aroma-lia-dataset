import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;

public class CheckstyleAntTaskTest extends AbstractPathTestSupport {

    @Test
    public void testCreateClasspath() {
        final CheckstyleAntTask antTask = new CheckstyleAntTask();

        assertEquals("", antTask.createClasspath().toString(), "Invalid classpath");

        antTask.setClasspath(new Path(new Project(), "/path"));

        assertEquals("", antTask.createClasspath().toString(), "Invalid classpath");
    }
}
