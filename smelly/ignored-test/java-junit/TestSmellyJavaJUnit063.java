import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class TokenControllerTests {

    @Disabled
    @Test
    void createReturnsToken() throws Exception {
        TokenViewModel vm = new TokenViewModel();
        vm.setEmail("admin@contoso.com");
        vm.setPassword("Pass@word1!");
        Object result = _sut.create(vm);
        assertTrue(result instanceof OkObjectResult);
        String token = ((OkObjectResult) result).getValue().toString();
        assertTrue(token.contains("token"));
    }
}
