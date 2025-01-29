import pytest
from unittest.mock import AsyncMock
from http import HTTPStatus

@pytest.mark.asyncio
async def test_should_delete_organization_unit_async():
    organization_unit_app_service_mock = AsyncMock()
    organization_unit_app_service_mock.delete_async.return_value = {
        "Id": "some-guid",
        "SelectedUsers": [{"Username": "test_user_for_delete_ou"}],
        "SelectedRoles": [{"RoleName": "test_role_for_delete_ou"}]
    }
    organization_units_controller = OrganizationUnitsController(organization_unit_app_service_mock)
    action_result = await organization_units_controller.delete_organization_units("some-guid")
    assert action_result.status_code == HTTPStatus.OK
    organization_unit_output = action_result.json()
    assert len(organization_unit_output["SelectedUsers"]) > 0
    assert len(organization_unit_output["SelectedRoles"]) > 0
