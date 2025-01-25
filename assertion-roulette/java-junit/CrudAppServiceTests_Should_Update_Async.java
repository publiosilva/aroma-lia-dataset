import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class CrudAppServiceTests {

    @Test
    public void shouldUpdateAsync() throws Exception {
        var dbContextForAddEntity = getDefaultTestDbContext();
        var productDto = dbContextForAddEntity.getProducts().add(new Product("update_product_code", "Update Product Name"));
        dbContextForAddEntity.saveChanges();

        var userOutput = _productCrudAppService.update(new UpdateProductInput(productDto.getId(), "update_product_code_updated", "Update Product Name Updated"));
        defaultTestDbContext.saveChanges();

        var dbContextForGetEntity = getDefaultTestDbContext();
        var updatedProductDto = dbContextForGetEntity.getProducts().findById(productDto.getId());

        assertNotNull(userOutput);
        assertNotNull(productDto);
        assertNotNull(updatedProductDto);
        assertEquals("update_product_code_updated", updatedProductDto.getCode());
        assertEquals("Update Product Name Updated", updatedProductDto.getName());
    }
}
