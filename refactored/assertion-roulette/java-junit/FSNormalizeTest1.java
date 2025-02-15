import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class FileSystemNormalizeTests {

    @Test
    public void FSNormalizeTest1() {
        {
            ScopeContext.pushProperty("foo", "abc.log");
            SimpleLayout l = "${filesystem-normalize:${scopeproperty:foo}}";
            assertEquals("Explanation message", "abc.log", l.render(LogEventInfo.createNullEvent()));
            ScopeContext.pushProperty("foo", "");
            assertEquals("Explanation message", "", l.render(LogEventInfo.createNullEvent()));
            ScopeContext.pushProperty("foo", "a/b/c");
            assertEquals("Explanation message", "a_b_c", l.render(LogEventInfo.createNullEvent()));
            ScopeContext.pushProperty("foo", ":\\/$@#$%^");
            assertEquals("Explanation message", "_________", l.render(LogEventInfo.createNullEvent()));
        }
    }
}
