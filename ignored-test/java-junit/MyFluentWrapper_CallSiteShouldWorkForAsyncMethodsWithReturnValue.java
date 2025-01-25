import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class MyFluentWrapper {

    @Test
    @Disabled("")
    public void callSiteShouldWorkForAsyncMethodsWithReturnValue() throws Exception {
        String callSite = getAsyncCallSite().get();
        assertEquals("NLog.UnitTests.LayoutRenderers.CallSiteTests.GetAsyncCallSite", callSite);
    }
}
