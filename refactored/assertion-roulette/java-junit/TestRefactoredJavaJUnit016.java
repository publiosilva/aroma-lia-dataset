import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class UsersControllerTests {

    @Test
    public void shouldGetUserListAsync() throws Exception {
        IUserAppService userAppServiceMock = mock(IUserAppService.class);
        when(userAppServiceMock.getListAsync(any(PagedListInput.class))).thenReturn(CompletableFuture.completedFuture(new PagedListResult<UserListOutput>() {{
            setItems(Arrays.asList(new UserListOutput("test_user_1", "test_user_mail_1@mail", UUID.randomUUID()),
                                     new UserListOutput("test_user_2", "test_user_mail_2@mail", UUID.randomUUID()),
                                     new UserListOutput("test_user_3", "test_user_mail_3@mail", UUID.randomUUID()),
                                     new UserListOutput("test_user_4", "test_user_mail_4@mail", UUID.randomUUID()),
                                     new UserListOutput("test_user_5", "test_user_mail_5@mail", UUID.randomUUID())));
            setTotalCount(10);
        }}));

        UsersController usersController = new UsersController(userAppServiceMock, mock(IIdentityAppService.class));
        var actionResult = usersController.getUsers(new PagedListInput()).get();

        assertTrue("Explanation message", actionResult instanceof OkObjectResult);
        var okObjectResult = (OkObjectResult) actionResult;
        assertTrue("Explanation message", okObjectResult.getValue() instanceof PagedListResult);
        var userPagedListResult = (PagedListResult<UserListOutput>) okObjectResult.getValue();
        assertEquals("Explanation message", HttpStatus.OK.value(), okObjectResult.getStatusCode());
        assertEquals("Explanation message", 10, userPagedListResult.getTotalCount());
        assertEquals("Explanation message", 5, userPagedListResult.getItems().size());
    }
}
