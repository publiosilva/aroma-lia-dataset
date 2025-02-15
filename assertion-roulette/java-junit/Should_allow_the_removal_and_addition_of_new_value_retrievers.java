import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.Test;

public class IExistsForTestingValueRetrieving {

    @Test
    public void shouldAllowTheRemovalAndAdditionOfNewValueRetrievers() {
        {
            Service service = new Service();
            for (ValueRetriever valueRetriever : service.getValueRetrievers().toArray(new ValueRetriever[0])) {
                service.getValueRetrievers().unregister(valueRetriever);
                assertFalse(service.getValueRetrievers().contains(valueRetriever));
            }

            IExistsForTestingValueRetrieving thing = new IExistsForTestingValueRetrieving();
            service.getValueRetrievers().register(thing);
            assertEquals(1, service.getValueRetrievers().size());
            assertSame(thing, service.getValueRetrievers().get(0));
        }
    }
}
