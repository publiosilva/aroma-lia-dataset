using Xunit;
using Moq;

public class TokenControllerTests {

    // [Fact(Skip = "")]
    [Fact]
    public void CreateReturnsToken() {
        var vm = new TokenViewModel();
        vm.Email = "admin@contoso.com";
        vm.Password = "Pass@word1!";
        var result = _sut.Create(vm);
        Assert.IsType<OkObjectResult>(result);
        var token = ((OkObjectResult)result).Value.ToString();
        Assert.Contains("token", token);
    }
}
