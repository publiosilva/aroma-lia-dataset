import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class CrudAppServiceTests {

    @Test
    public void shouldCreateAsync() throws Exception {
        var userOutput = _productCrudAppService.createAsync(new CreateProductInput("create_async_product_code", "Create Async Product Name"));
        defaultTestDbContext.saveChanges();
        var anotherScopeDbContext = getDefaultTestDbContext();
        var insertedProductDto = anotherScopeDbContext.getProducts().findById(userOutput.getId());
        assertNotNull(userOutput);
        assertNotNull(insertedProductDto);
        assertEquals(userOutput.getCode(), insertedProductDto.getCode());
    }
}
