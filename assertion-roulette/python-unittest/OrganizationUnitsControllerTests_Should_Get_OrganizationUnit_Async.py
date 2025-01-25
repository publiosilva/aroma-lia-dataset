import unittest
from unittest.mock import Mock

class TestOrganizationUnitsController(unittest.TestCase):
    def test_should_get_organization_unit(self):
        organization_unit_app_service_mock = Mock()
        organization_unit_app_service_mock.get_async.return_value = OrganizationUnitOutput(
            id=str(uuid.uuid4()),
            selected_users=[get_test_user_output("test_user_for_get_ou")],
            selected_roles=[get_test_role_output("test_role_for_get_ou")]
        )
        
        organization_units_controller = OrganizationUnitsController(organization_unit_app_service_mock)
        action_result = organization_units_controller.get_organization_units(str(uuid.uuid4()))
        
        ok_object_result = self.assertIsInstance(action_result.result, OkObjectResult)
        organization_unit_output = self.assertIsInstance(ok_object_result.value, OrganizationUnitOutput)
        
        self.assertEqual(200, ok_object_result.status_code)
        self.assertTrue(len(organization_unit_output.selected_users) > 0)
        self.assertTrue(len(organization_unit_output.selected_roles) > 0)
