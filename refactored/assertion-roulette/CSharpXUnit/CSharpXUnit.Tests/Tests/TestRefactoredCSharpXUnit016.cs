using Xunit;
using Moq;
using System.Threading.Tasks;

public class UsersControllerTests 
{
    [Fact]
    public async Task ShouldGetUserListAsync() 
    {
        var userAppServiceMock = new Mock<IUserAppService>();
        userAppServiceMock
            .Setup(s => s.GetListAsync(It.IsAny<PagedListInput>()))
            .ReturnsAsync(new PagedListResult<UserListOutput>
            {
                Items = new List<UserListOutput>
                {
                    new UserListOutput("test_user_1", "test_user_mail_1@mail", Guid.NewGuid()),
                    new UserListOutput("test_user_2", "test_user_mail_2@mail", Guid.NewGuid()),
                    new UserListOutput("test_user_3", "test_user_mail_3@mail", Guid.NewGuid()),
                    new UserListOutput("test_user_4", "test_user_mail_4@mail", Guid.NewGuid()),
                    new UserListOutput("test_user_5", "test_user_mail_5@mail", Guid.NewGuid())
                },
                TotalCount = 10
            });

        var usersController = new UsersController(userAppServiceMock.Object, new Mock<IIdentityAppService>().Object);
        var actionResult = await usersController.GetUsers(new PagedListInput());

        Assert.IsType<OkObjectResult>(actionResult, "Explanation message");
        var okObjectResult = (OkObjectResult)actionResult;
        Assert.IsType<PagedListResult<UserListOutput>>(okObjectResult.Value, "Explanation message");
        var userPagedListResult = (PagedListResult<UserListOutput>)okObjectResult.Value;
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode, "Explanation message");
        Assert.Equal(10, userPagedListResult.TotalCount, "Explanation message");
        Assert.Equal(5, userPagedListResult.Items.Count, "Explanation message");
    }
}
