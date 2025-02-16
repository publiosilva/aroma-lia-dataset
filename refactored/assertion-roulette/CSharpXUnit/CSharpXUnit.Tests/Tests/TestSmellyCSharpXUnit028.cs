using Xunit;

namespace DefaultNamespace
{
    public class EditUserSuccessTest
    {
        [Fact]
        public void UpdateAsync_UpdateUser_User1()
        {
            {
                // Arrange
                var getableUser = new Mock<IGetableUser>();
                var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
                var editUser = new EditUser(getableUser.Object, userRepository.Object);
                getableUser.Setup(gu => gu.GetById(_user.Id)).Returns(_user);
                _user.Name = "TestName";
                // Act
                var result = await editUser.UpdateAsync(_user);
                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void UpdateAsync_UpdateUser_User2()
        {
            {
                // Arrange
                var getableUser = new Mock<IGetableUser>();
                var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
                var editUser = new EditUser(getableUser.Object, userRepository.Object);
                getableUser.Setup(gu => gu.GetById(_user.Id)).Returns(_user);
                _user.Name = "TestName";
                // Act
                var result = await editUser.UpdateAsync(_user);
                // Assert
                Assert.Equal(_user, result);
            }
        }

        [Fact]
        public void UpdateAsync_UpdateUser_User2()
        {
            {
                // Arrange
                var getableUser = new Mock<IGetableUser>();
                var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
                var editUser = new EditUser(getableUser.Object, userRepository.Object);
                getableUser.Setup(gu => gu.GetById(_user.Id)).Returns(_user);
                _user.Name = "TestName";
                // Act
                var result = await editUser.UpdateAsync(_user);
                // Assert
                Assert.Equal(_user.Id, result.Id);
            }
        }

        [Fact]
        public void UpdateAsync_UpdateUser_User4()
        {
            {
                // Arrange
                var getableUser = new Mock<IGetableUser>();
                var userRepository = new Mock<IRepositoryAsync<UserEntity>>();
                var editUser = new EditUser(getableUser.Object, userRepository.Object);
                getableUser.Setup(gu => gu.GetById(_user.Id)).Returns(_user);
                _user.Name = "TestName";
                // Act
                var result = await editUser.UpdateAsync(_user);
                // Assert
                Assert.Equal(_user.Name, result.Name);
            }
        }
    }
}
