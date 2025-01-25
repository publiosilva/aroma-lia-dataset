import unittest
from unittest.mock import Mock

class TestOrganizationUnitsController(unittest.TestCase):
    def test_should_get_organization_unit_list(self):
        organization_unit_app_service_mock = Mock()
        organization_unit_app_service_mock.get_list_async.return_value = PagedListResult(
            items=[
                OrganizationUnitListOutput(name="test_organizationUnit_1", id=str(uuid.uuid4())),
                OrganizationUnitListOutput(name="test_organizationUnit_2", id=str(uuid.uuid4())),
                OrganizationUnitListOutput(name="test_organizationUnit_3", id=str(uuid.uuid4())),
                OrganizationUnitListOutput(name="test_organizationUnit_4", id=str(uuid.uuid4())),
                OrganizationUnitListOutput(name="test_organizationUnit_5", id=str(uuid.uuid4()))
            ],
            total_count=10
        )
        
        organization_units_controller = OrganizationUnitsController(organization_unit_app_service_mock)
        action_result = organization_units_controller.get_organization_units(PagedListInput())
        
        ok_object_result = self.assertIsInstance(action_result.result, OkObjectResult)
        organization_unit_paged_list_result = self.assertIsInstance(ok_object_result.value, PagedListResult)
        
        self.assertEqual(200, ok_object_result.status_code)
        self.assertEqual(10, organization_unit_paged_list_result.total_count)
        self.assertEqual(5, len(organization_unit_paged_list_result.items))
