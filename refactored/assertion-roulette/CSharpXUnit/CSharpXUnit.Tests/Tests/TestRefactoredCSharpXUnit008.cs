using Xunit;

public class CustomerProfileTest
{
    [Fact]
    public void customerFilterToCustomerFilterDto()
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

        Assert.Equal(customerFilterDto.FirstName, customerFilter.FirstName, "Explanation message");
        Assert.Equal(customerFilterDto.Surname, customerFilter.Surname, "Explanation message");
        Assert.Equal(customerFilterDto.Id, customerFilter.Id, "Explanation message");
        Assert.Equal(customerFilterDto.Email, customerFilter.Email, "Explanation message");
        Assert.Equal(customerFilterDto.CurrentPage, customerFilter.CurrentPage, "Explanation message");
        Assert.Equal(customerFilterDto.OrderBy, customerFilter.OrderBy, "Explanation message");
        Assert.Equal(customerFilterDto.PageSize, customerFilter.PageSize, "Explanation message");
        Assert.Equal(customerFilterDto.SortBy, customerFilter.SortBy, "Explanation message");
    }
}
