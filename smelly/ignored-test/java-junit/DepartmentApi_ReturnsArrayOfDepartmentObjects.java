import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.condition.EnabledIf;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class ApiIntegrationTests {

    @Test
    @EnabledIf("false") // Replace with the proper condition if needed
    public void departmentApi_ReturnsArrayOfDepartmentObjects() throws Exception {
        String url = "/Departments";
        try (var th = initTestServer()) {
            var client = th.createClient();
            var response = client.getAsync(url).join();
            assertEquals(HttpStatusCode.OK, response.getStatusCode());
            var content = response.getContent().readAsStringAsync().join();
            assertTrue(content.contains("English"));
        }
    }
}
