import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.time.LocalDate;

public class GlobalQueryFiltersTests {

    @Test
    public void shouldAddModificationAuditingProperties() throws Exception {
        var result = defaultTestDbContext.getProducts().add(new Product("Product Name", "product_code"));
        defaultTestDbContext.saveChanges();

        defaultTestDbContext.getProducts().update(result.getEntity());
        defaultTestDbContext.saveChanges();

        var dbContextToGetUpdatedEntity = getDefaultTestDbContext();
        var updatedEntity = dbContextToGetUpdatedEntity.getProducts().findById(result.getEntity().getId());

        assertNotNull(updatedEntity.getModifierUserId());
        assertNotEquals(java.util.UUID.randomUUID(), updatedEntity.getModifierUserId());
        assertNotNull(updatedEntity.getModificationTime());
        assertEquals(LocalDate.now().toString(), updatedEntity.getModificationTime().toLocalDate().toString());
    }
}
