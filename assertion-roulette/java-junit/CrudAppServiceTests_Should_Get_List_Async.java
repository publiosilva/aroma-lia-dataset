import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.time.LocalDateTime;
import java.util.Arrays;

public class CrudAppServiceTests {

    @Test
    public void shouldGetListAsync() throws Exception {
        defaultTestDbContext.getProducts().add(new Product("E Product Name", "e_product_code_for_get_list_with_filter_and_sort_async"));
        defaultTestDbContext.getProducts().add(new Product("A Product Name", "a_product_code_for_get_list_with_filter_and_sort_async"));
        defaultTestDbContext.getProducts().add(new Product("B Product Name 1", "b_product_code_1_for_get_list_with_filter_and_sort_async"));
        defaultTestDbContext.getProducts().add(new Product("B Product Name 1", "b_product_code_2_for_get_list_with_filter_and_sort_async"));
        defaultTestDbContext.saveChanges();

        var pagedListInput = new PagedListInput();
        pagedListInput.setFilters(Arrays.asList(
            "Name.contains(\"Product\")",
            "CreationTime > LocalDateTime.now().minusMinutes(1)",
            "Code.contains(\"for_get_list_with_filter_and_sort_async\")"
        ));
        pagedListInput.setSorts(Arrays.asList(
            "Name",
            "Code desc"
        ));

        var pagedProductList = _productCrudAppService.getListAsync(pagedListInput);
        assertNotNull(pagedProductList);
        assertEquals(4, pagedProductList.getTotalCount());
        assertEquals("b_product_code_2_for_get_list_with_filter_and_sort_async", pagedProductList.getItems().get(1).getCode());
    }
}
