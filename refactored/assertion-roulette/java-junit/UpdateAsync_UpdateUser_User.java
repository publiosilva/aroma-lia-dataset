import org.junit.jupiter.api.Test;
import static org.mockito.Mockito.*;
import static org.junit.jupiter.api.Assertions.*;

public class EditUserSuccessTest {

    @Test
    public void updateAsync_UpdateUser_User() {
        // Arrange
        IGetableUser getableUser = mock(IGetableUser.class);
        IRepositoryAsync<UserEntity> userRepository = mock(IRepositoryAsync.class);
        EditUser editUser = new EditUser(getableUser, userRepository);
        when(getableUser.getById(_user.getId())).thenReturn(_user);
        _user.setName("TestName");
        
        // Act
        UserEntity result = editUser.updateAsync(_user);
        
        // Assert
        assertNotNull("Explanation message", result);
        assertEquals("Explanation message", _user, result);
        assertEquals("Explanation message", _user.getId(), result.getId());
        assertEquals("Explanation message", _user.getName(), result.getName());
    }
}
