import org.junit.Test;
import static org.junit.Assert.*;

@Disabled
public class LayoutTypedTests {

    @Test
    public void renderShouldRecognizeStackTraceUsage() {
        // Arrange
        Object[] callbackArgs = null;
        BiConsumer<LogEventInfo, Object[]> callback = (evt, args) -> callbackArgs = args;
        Logger logger = new LogFactory().setup().loadConfiguration(builder -> {
            MethodCallTarget methodCall = new MethodCallTarget("dbg", callback);
            methodCall.getParameters().add(new MethodCallParameter("LineNumber", "${callsite-linenumber}", Integer.class));
            builder.forLogger().writeTo(methodCall);
        }).getLogger("renderShouldRecognizeStackTraceUsage");
        // Act
        logger.info("Testing");
        // Assert
        assertEquals(1, callbackArgs.length);
        int lineNumber = (Integer) callbackArgs[0];
        assertTrue(lineNumber > 0);
    }
}
