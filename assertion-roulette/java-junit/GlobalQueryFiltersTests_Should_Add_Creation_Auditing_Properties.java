import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.time.LocalDate;

public class GlobalQueryFiltersTests {

    @Test
    public void shouldAddCreationAuditingProperties() throws Exception {
        var result = defaultTestDbContext.getProducts().add(new Product("Product Name", "product_code"));
        defaultTestDbContext.saveChanges();

        assertNotNull(result.getEntity().getCreatorUserId());
        assertNotEquals(java.util.UUID.randomUUID(), result.getEntity().getCreatorUserId());
        assertEquals(LocalDate.now().toString(), result.getEntity().getCreationTime().toLocalDate().toString());
    }
}
