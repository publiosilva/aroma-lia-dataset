using Xunit;

namespace BackOfficeBase.Tests.Web.Api
{
    public class OrganizationUnitsControllerTests
    {
        [Fact]
        public void Should_Get_OrganizationUnit_List_Async()
        {
            {
                var organizationUnitAppServiceMock = new Mock<IOrganizationUnitAppService>();
                organizationUnitAppServiceMock.Setup(x => x.GetListAsync(It.IsAny<PagedListInput>())).ReturnsAsync(new PagedListResult<OrganizationUnitListOutput> { Items = new List<OrganizationUnitListOutput> { new OrganizationUnitListOutput { Name = "test_organizationUnit_1", Id = Guid.NewGuid() }, new OrganizationUnitListOutput { Name = "test_organizationUnit_2", Id = Guid.NewGuid() }, new OrganizationUnitListOutput { Name = "test_organizationUnit_3", Id = Guid.NewGuid() }, new OrganizationUnitListOutput { Name = "test_organizationUnit_4", Id = Guid.NewGuid() }, new OrganizationUnitListOutput { Name = "test_organizationUnit_5", Id = Guid.NewGuid() } }, TotalCount = 10 });
                var organizationUnitsController = new OrganizationUnitsController(organizationUnitAppServiceMock.Object);
                var actionResult = await organizationUnitsController.GetOrganizationUnits(new PagedListInput());
                var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var organizationUnitPagedListResult = Assert.IsType<PagedListResult<OrganizationUnitListOutput>>(okObjectResult.Value);
                Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
                Assert.Equal(10, organizationUnitPagedListResult.TotalCount);
                Assert.Equal(5, organizationUnitPagedListResult.Items.Count());
            }
        }
    }
}
