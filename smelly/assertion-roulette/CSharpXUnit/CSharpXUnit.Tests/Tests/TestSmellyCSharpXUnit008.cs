using Xunit;

namespace DefaultNamespace
{
    public class CustomerProfileTest
    {
        [Fact]
        public void CustomerFilterToCustomerFilterDto()
        {
            {
                var customerFilterDto = new CustomerFilterDto
                {
                    Surname = "Dickinson",
                    FirstName = "Bruce",
                    Email = "maiden@metal.com",
                    Id = 1,
                    CurrentPage = 1,
                    PageSize = 1,
                    OrderBy = "desc",
                    SortBy = "firstName"
                };
                var mapper = MapperConfiguration.CreateMapper();
                var customerFilter = mapper.Map<CustomerFilter>(customerFilterDto);
                Assert.Equal(customerFilterDto.FirstName, customerFilter.FirstName);
                Assert.Equal(customerFilterDto.Surname, customerFilter.Surname);
                Assert.Equal(customerFilterDto.Id, customerFilter.Id);
                Assert.Equal(customerFilterDto.Email, customerFilter.Email);
                Assert.Equal(customerFilterDto.CurrentPage, customerFilter.CurrentPage);
                Assert.Equal(customerFilterDto.OrderBy, customerFilter.OrderBy);
                Assert.Equal(customerFilterDto.PageSize, customerFilter.PageSize);
                Assert.Equal(customerFilterDto.SortBy, customerFilter.SortBy);
            }
        }
    }
}
