import org.junit.Test;
import static org.junit.Assert.*;

public class AspMemoryCacheTests {
    @Test
    public void should_clear_key_if_ttl_expired() {
        {
            Fake fake = new Fake(1);
            _cache.add("1", fake, 50, "region");
            // Thread.sleep(200);
            Object result = _cache.get("1", "region");
            assertNull(result);
        }
    }
}