import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

public class AsyncHelperTests {

    @Test
    public void continuationTimeoutNotHitTest() {
        {
            List<Exception> exceptions = new ArrayList<>();
            var cont = AsyncHelpers.withTimeout(AsyncHelpers.preventMultipleCalls(exceptions::add), 50);
            cont.accept(null);
            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
            assertEquals(1, exceptions.size());
            assertNull(exceptions.get(0));
            cont.accept(null);
            cont.accept(new ApplicationException("Some exception"));
            cont.accept(null);
            cont.accept(new ApplicationException("Some exception"));
            assertEquals(1, exceptions.size());
            assertNull(exceptions.get(0));
        }
    }
}
