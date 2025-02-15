using Xunit;

namespace DefaultNamespace
{
    public class ApiIntegrationTests
    {
        [Fact(Skip = "")]
        public void DepartmentApi_ReturnsArrayOfDepartmentObjects()
        {
            {
                var url = "/Departments";
                using (var th = InitTestServer())
                {
                    var client = th.CreateClient();
                    var response = await client.GetAsync(url);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    var content = await response.Content.ReadAsStringAsync();
                    Assert.True(content.Contains("English"));
                }
            }
        }
    }
}
