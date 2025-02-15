using Xunit;

namespace DefaultNamespace
{
    public class UsersControllerTests
    {
        [Fact]
        public void Should_Get_User_List_Async()
        {
            {
                var userAppServiceMock = new Mock<IUserAppService>();
                userAppServiceMock.Setup(x => x.GetListAsync(It.IsAny<PagedListInput>())).ReturnsAsync(new PagedListResult<UserListOutput> { Items = new List<UserListOutput> { new UserListOutput { UserName = "test_user_1", Email = "test_user_mail_1@mail", Id = Guid.NewGuid() }, new UserListOutput { UserName = "test_user_2", Email = "test_user_mail_2@mail", Id = Guid.NewGuid() }, new UserListOutput { UserName = "test_user_3", Email = "test_user_mail_3@mail", Id = Guid.NewGuid() }, new UserListOutput { UserName = "test_user_4", Email = "test_user_mail_4@mail", Id = Guid.NewGuid() }, new UserListOutput { UserName = "test_user_5", Email = "test_user_mail_5@mail", Id = Guid.NewGuid() } }, TotalCount = 10 });
                var usersController = new UsersController(userAppServiceMock.Object, new Mock<IIdentityAppService>().Object);
                var actionResult = await usersController.GetUsers(new PagedListInput());
                var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var userPagedListResult = Assert.IsType<PagedListResult<UserListOutput>>(okObjectResult.Value);
                Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
                Assert.Equal(10, userPagedListResult.TotalCount);
                Assert.Equal(5, userPagedListResult.Items.Count());
            }
        }
    }
}
