import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class CheckstyleAntTaskTest extends AbstractPathTestSupport {

    @Test
    public void testCreateClasspath() {
        CheckstyleAntTask antTask = new CheckstyleAntTask();

        assertEquals("", antTask.CreateClasspath().toString(), "Invalid classpath");

        antTask.setClasspath(new Path(new Project(), "/path"));

        assertEquals("", antTask.CreateClasspath().toString(), "Invalid classpath");
    }
}