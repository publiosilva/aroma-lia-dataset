using Moq;
using Xunit;

public class EditUserSuccessTest {

    [Fact]
    public void updateAsync_UpdateUser_User() {
        // Arrange
        var getableUser = new Mock<IGetableUser>();
        var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
        var editUser = new EditUser(getableUser.Object, userRepository.Object);
        getableUser.Setup(x => x.getById(_user.getId())).Returns(_user);
        _user.setName("TestName");

        // Act
        var result = editUser.updateAsync(_user);

        // Assert
        Assert.NotNull(result, "Explanation message");
        Assert.Equal(_user, result, "Explanation message");
        Assert.Equal(_user.getId(), result.getId(), "Explanation message");
        Assert.Equal(_user.getName(), result.getName(), "Explanation message");
    }
}
