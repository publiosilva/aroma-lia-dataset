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

        assertEquals(customerFilterDto.getFirstName(), customerFilter.getFirstName());
        assertEquals(customerFilterDto.getSurname(), customerFilter.getSurname());
        assertEquals(customerFilterDto.getId(), customerFilter.getId());
        assertEquals(customerFilterDto.getEmail(), customerFilter.getEmail());
        assertEquals(customerFilterDto.getCurrentPage(), customerFilter.getCurrentPage());
        assertEquals(customerFilterDto.getOrderBy(), customerFilter.getOrderBy());
        assertEquals(customerFilterDto.getPageSize(), customerFilter.getPageSize());
        assertEquals(customerFilterDto.getSortBy(), customerFilter.getSortBy());
    }
}
