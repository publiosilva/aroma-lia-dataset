import org.junit.Test;
import static org.junit.Assert.*;

public class ValueFormatterTest {

    @Test
    public void testSerialisationOfRecursiveClassObjectToJsonIsSuccessful() {
        {
            RecursiveTest classObj = new RecursiveTest(0);
            StringBuilder builder = new StringBuilder();
            boolean result = createValueFormatter().formatValue(classObj, "", CaptureType.Serialize, null, builder);
            assertTrue(result);
            String actual = builder.toString();
            String deepestInteger = "\"Integer\":10";
            assertTrue(actual.contains(deepestInteger));
            String deepestNext = "\"Next\":\"NLog.UnitTests.MessageTemplates.ValueFormatterTest+RecursiveTest\"";
            assertTrue(actual.contains(deepestNext));
        }
    }
}
