import pytest
from unittest.mock import AsyncMock
from http import HTTPStatus

@pytest.mark.asyncio
async def test_should_get_organization_unit_async():
    organization_unit_app_service_mock = AsyncMock()
    organization_unit_app_service_mock.get_async.return_value = {
        "Id": "some-guid",
        "SelectedUsers": [{"Username": "test_user_for_get_ou"}],
        "SelectedRoles": [{"RoleName": "test_role_for_get_ou"}]
    }
    organization_units_controller = OrganizationUnitsController(organization_unit_app_service_mock)
    action_result = await organization_units_controller.get_organization_units("some-guid")
    assert action_result.status_code == HTTPStatus.OK
    organization_unit_output = action_result.json()
    assert len(organization_unit_output["SelectedUsers"]) > 0
    assert len(organization_unit_output["SelectedRoles"]) > 0
