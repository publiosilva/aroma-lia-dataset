using Xunit;

namespace DefaultNamespace
{
    public class TokenControllerTests
    {
        [Fact(Skip = "")]
        public void Create_ReturnsToken()
        {
            {
                var vm = new TokenViewModel
                {
                    Email = "admin@contoso.com",
                    Password = "Pass@word1!"
                };
                var result = await _sut.Create(vm);
                Assert.IsType<OkObjectResult>(result);
                string token = ((OkObjectResult)result).Value.ToString();
                Assert.Contains("token", token);
            }
        }
    }
}
