import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

import java.util.ArrayList;
import java.util.List;

public class AsyncHelperTests {

    @Test
    public void continuationTimeoutNotHitTest() throws Exception {
        List<Exception> exceptions = new ArrayList<>();

        RunnableWithTimeout cont = AsyncHelpers.withTimeout(
            AsyncHelpers.preventMultipleCalls(exceptions::add),
            50
        );

        cont.run(null);
        Thread.sleep(100);

        assertEquals(1, exceptions.size());
        assertNull(exceptions.get(0));

        cont.run(null);
        cont.run(new ApplicationException("Some exception"));
        cont.run(null);
        cont.run(new ApplicationException("Some exception"));

        assertEquals(1, exceptions.size());
        assertNull(exceptions.get(0));
    }
}
