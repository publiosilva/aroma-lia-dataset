import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertSame;

import java.util.ArrayList;
import java.util.List;

public class AsyncHelperTests {

    @Test
    public void continuationErrorTimeoutNotHitTest() throws Exception {
        List<Exception> exceptions = new ArrayList<>();

        RunnableWithTimeout cont = AsyncHelpers.withTimeout(
            AsyncHelpers.preventMultipleCalls(exceptions::add),
            50
        );

        ApplicationException exception = new ApplicationException("Foo");

        cont.run(exception);
        Thread.sleep(100);

        assertEquals(1, exceptions.size());
        assertNotNull(exceptions.get(0));
        assertSame(exception, exceptions.get(0));

        cont.run(null);
        cont.run(new ApplicationException("Some exception"));
        cont.run(null);
        cont.run(new ApplicationException("Some exception"));

        assertEquals(1, exceptions.size());
        assertNotNull(exceptions.get(0));
    }
}
