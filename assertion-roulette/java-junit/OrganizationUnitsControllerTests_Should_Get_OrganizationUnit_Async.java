import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

import org.springframework.http.ResponseEntity;
import java.util.UUID;
import java.util.Arrays;

public class OrganizationUnitsControllerTests {

    @Test
    public void shouldGetOrganizationUnitAsync() throws Exception {
        var organizationUnitAppServiceMock = mock(IOrganizationUnitAppService.class);
        when(organizationUnitAppServiceMock.getAsync(any(UUID.class)))
            .thenReturn(new OrganizationUnitOutput(UUID.randomUUID(), 
                Arrays.asList(getTestUserOutput("test_user_for_get_ou")), 
                Arrays.asList(getTestRoleOutput("test_role_for_get_ou"))));

        var organizationUnitsController = new OrganizationUnitsController(organizationUnitAppServiceMock);
        ResponseEntity<OrganizationUnitOutput> actionResult = organizationUnitsController.getOrganizationUnits(UUID.randomUUID());

        assertEquals(200, actionResult.getStatusCodeValue());
        var organizationUnitOutput = actionResult.getBody();
        assertNotNull(organizationUnitOutput);
        assertTrue(organizationUnitOutput.getSelectedUsers().size() > 0);
        assertTrue(organizationUnitOutput.getSelectedRoles().size() > 0);
    }
}
