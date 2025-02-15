import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class FileSystemNormalizeTests {

    @Test
    public void FSNormalizeTest1() {
        {
            ScopeContext.pushProperty("foo", "abc.log");
            SimpleLayout l = "${filesystem-normalize:${scopeproperty:foo}}";
            assertEquals("abc.log", l.render(LogEventInfo.createNullEvent()));
            ScopeContext.pushProperty("foo", "");
            assertEquals("", l.render(LogEventInfo.createNullEvent()));
            ScopeContext.pushProperty("foo", "a/b/c");
            assertEquals("a_b_c", l.render(LogEventInfo.createNullEvent()));
            ScopeContext.pushProperty("foo", ":\\/$@#$%^");
            assertEquals("_________", l.render(LogEventInfo.createNullEvent()));
        }
    }
}
