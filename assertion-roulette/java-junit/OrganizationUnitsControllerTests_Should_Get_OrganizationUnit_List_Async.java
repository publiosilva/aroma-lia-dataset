import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

import org.springframework.http.ResponseEntity;
import java.util.UUID;
import java.util.Arrays;

public class OrganizationUnitsControllerTests {

    @Test
    public void shouldGetOrganizationUnitListAsync() throws Exception {
        var organizationUnitAppServiceMock = mock(IOrganizationUnitAppService.class);
        when(organizationUnitAppServiceMock.getListAsync(any(PagedListInput.class)))
            .thenReturn(new PagedListResult<>(
                Arrays.asList(
                    new OrganizationUnitListOutput("test_organizationUnit_1", UUID.randomUUID()),
                    new OrganizationUnitListOutput("test_organizationUnit_2", UUID.randomUUID()),
                    new OrganizationUnitListOutput("test_organizationUnit_3", UUID.randomUUID()),
                    new OrganizationUnitListOutput("test_organizationUnit_4", UUID.randomUUID()),
                    new OrganizationUnitListOutput("test_organizationUnit_5", UUID.randomUUID())
                ),
                10
            ));

        var organizationUnitsController = new OrganizationUnitsController(organizationUnitAppServiceMock);
        ResponseEntity<PagedListResult<OrganizationUnitListOutput>> actionResult = organizationUnitsController.getOrganizationUnits(new PagedListInput());

        assertEquals(200, actionResult.getStatusCodeValue());
        var organizationUnitPagedListResult = actionResult.getBody();
        assertNotNull(organizationUnitPagedListResult);
        assertEquals(10, organizationUnitPagedListResult.getTotalCount());
        assertEquals(5, organizationUnitPagedListResult.getItems().size());
    }
}
