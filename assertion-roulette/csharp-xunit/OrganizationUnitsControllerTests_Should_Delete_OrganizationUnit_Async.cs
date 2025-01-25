using Xunit;

namespace BackOfficeBase.Tests.Web.Api
{
    public class OrganizationUnitsControllerTests
    {
        [Fact]
        public void Should_Delete_OrganizationUnit_Async()
        {
            {
                var organizationUnitAppServiceMock = new Mock<IOrganizationUnitAppService>();
                organizationUnitAppServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(new OrganizationUnitOutput { Id = Guid.NewGuid(), SelectedUsers = new[] { GetTestUserOutput("test_user_for_delete_ou") }, SelectedRoles = new[] { GetTestRoleOutput("test_role_for_delete_ou") } });
                var organizationUnitsController = new OrganizationUnitsController(organizationUnitAppServiceMock.Object);
                var actionResult = await organizationUnitsController.DeleteOrganizationUnits(Guid.NewGuid());
                var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var organizationUnitOutput = Assert.IsType<OrganizationUnitOutput>(okObjectResult.Value);
                Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
                Assert.True(organizationUnitOutput.SelectedUsers.Any());
                Assert.True(organizationUnitOutput.SelectedRoles.Any());
            }
        }
    }
}
