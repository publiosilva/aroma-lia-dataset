using Xunit;
using System.Net;
using System.Threading.Tasks;

public class ApiIntegrationTests
{
    [Fact]
    // [Fact(Skip = "Replace with the proper condition if needed")]
    public async Task DepartmentApi_ReturnsArrayOfDepartmentObjects()
    {
        string url = "/Departments";
        using (var th = InitTestServer())
        {
            var client = th.CreateClient();
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("English", content);
        }
    }
}
