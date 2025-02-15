import org.junit.Test;
import org.junit.Ignore;
import static org.junit.Assert.assertTrue;

public class ClassStp {
    @Ignore("This is ignored with cause, no failure")
    @Test
    public void TestOneOneIgnoredWithCause() {
        assertTrue(1 == 2);
    }
}
