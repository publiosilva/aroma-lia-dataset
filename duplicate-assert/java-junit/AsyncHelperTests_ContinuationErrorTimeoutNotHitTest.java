import org.junit.Test;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertSame;
import static org.junit.Assert.assertEquals;

import java.util.ArrayList;
import java.util.List;

public class AsyncHelperTests {
    @Test
    public void continuationErrorTimeoutNotHitTest() {
        {
            List<Exception> exceptions = new ArrayList<>();
            // set up a timer to strike
            var cont = AsyncHelpers.WithTimeout(AsyncHelpers.PreventMultipleCalls(exceptions::add), 50);
            Exception exception = new ApplicationException("Foo");
            // call success quickly, hopefully before the timer comes
            cont.apply(exception);
            // sleep to make sure timer event comes
            Thread.sleep(100);
            // make sure we got success, not a timer exception
            assertEquals(1, exceptions.size());
            assertNotNull(exceptions.get(0));
            assertSame(exception, exceptions.get(0));
            // those will be ignored
            cont.apply(null);
            cont.apply(new ApplicationException("Some exception"));
            cont.apply(null);
            cont.apply(new ApplicationException("Some exception"));
            assertEquals(1, exceptions.size());
            assertNotNull(exceptions.get(0));
        }
    }
}
