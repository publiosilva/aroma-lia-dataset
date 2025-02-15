import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class CustomerProfileTest {

    @Test
    public void customerFilterToCustomerFilterDto() {
        CustomerFilterDto customerFilterDto = new CustomerFilterDto();
        customerFilterDto.setSurname("Dickinson");
        customerFilterDto.setFirstName("Bruce");
        customerFilterDto.setEmail("maiden@metal.com");
        customerFilterDto.setId(1);
        customerFilterDto.setCurrentPage(1);
        customerFilterDto.setPageSize(1);
        customerFilterDto.setOrderBy("desc");
        customerFilterDto.setSortBy("firstName");

        var mapper = MapperConfiguration.createMapper();
        var customerFilter = mapper.map(customerFilterDto, CustomerFilter.class);

        assertEquals("Explanation message", customerFilterDto.getFirstName(), customerFilter.getFirstName());
        assertEquals("Explanation message", customerFilterDto.getSurname(), customerFilter.getSurname());
        assertEquals("Explanation message", customerFilterDto.getId(), customerFilter.getId());
        assertEquals("Explanation message", customerFilterDto.getEmail(), customerFilter.getEmail());
        assertEquals("Explanation message", customerFilterDto.getCurrentPage(), customerFilter.getCurrentPage());
        assertEquals("Explanation message", customerFilterDto.getOrderBy(), customerFilter.getOrderBy());
        assertEquals("Explanation message", customerFilterDto.getPageSize(), customerFilter.getPageSize());
        assertEquals("Explanation message", customerFilterDto.getSortBy(), customerFilter.getSortBy());
    }
}
