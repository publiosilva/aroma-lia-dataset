import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertThrows;

public class InternalLoggerTests_Trace {

    @Test
    @Disabled("")
    public void shouldThrowStackOverflowExceptionWhenUsingNLogTraceListener() {
        setupTestConfiguration(NLogTraceListener.class, LogLevel.TRACE, true, null);
        assertThrows(StackOverflowError.class, () -> Trace.writeLine("StackOverFlowException"));
    }
}
