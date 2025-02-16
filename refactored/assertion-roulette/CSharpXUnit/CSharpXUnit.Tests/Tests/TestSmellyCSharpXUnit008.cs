using Xunit;

namespace DefaultNamespace
{
    public class CustomerProfileTest
    {
        [Fact]
        public void CustomerFilterToCustomerFilterDto1()
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
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto2()
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
                Assert.Equal(customerFilterDto.Surname, customerFilter.Surname);
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto3()
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
                Assert.Equal(customerFilterDto.Id, customerFilter.Id);
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto4()
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
                Assert.Equal(customerFilterDto.Email, customerFilter.Email);
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto5()
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
                Assert.Equal(customerFilterDto.CurrentPage, customerFilter.CurrentPage);
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto6()
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
                Assert.Equal(customerFilterDto.OrderBy, customerFilter.OrderBy);
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto7()
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
                Assert.Equal(customerFilterDto.PageSize, customerFilter.PageSize);
            }
        }

        [Fact]
        public void CustomerFilterToCustomerFilterDto8()
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
                Assert.Equal(customerFilterDto.SortBy, customerFilter.SortBy);
            }
        }
    }
}
