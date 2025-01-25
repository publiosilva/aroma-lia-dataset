import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.time.LocalDate;

public class GlobalQueryFiltersTests {

    @Test
    public void shouldAddDeletionAuditingProperties() throws Exception {
        var result = defaultTestDbContext.getProducts().add(new Product("Product Name", "product_code"));
        defaultTestDbContext.saveChanges();

        var deletedEntity = defaultTestDbContext.getProducts().remove(result.getEntity());
        defaultTestDbContext.saveChanges();

        assertNotNull(deletedEntity.getEntity().getDeleterUserId());
        assertNotEquals(java.util.UUID.randomUUID(), deletedEntity.getEntity().getDeleterUserId());
        assertNotNull(deletedEntity.getEntity().getDeletionTime());
        assertEquals(LocalDate.now().toString(), deletedEntity.getEntity().getDeletionTime().toLocalDate().toString());
    }
}
