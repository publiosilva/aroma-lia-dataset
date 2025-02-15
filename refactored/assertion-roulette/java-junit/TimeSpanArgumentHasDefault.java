import org.junit.Test;
import static org.junit.Assert.*;

public class ArgumentTests {

    @Test
    public void timeSpanArgumentHasDefault() {
        {
            SampleTimeSpanArgument arg = new SampleTimeSpanArgument(Duration.ofMinutes(3));
            UnitTestCommand command = UnitTestCommand.fromArgument(arg);
            int exitCode = command.invoke(new String[0]);
            assertEquals("Explanation message", 0, exitCode);
            assertTrue("Explanation message", command.isCommandRun());
            assertEquals("Explanation message", Duration.ofMinutes(3), arg.getValue());
        }
    }
}
