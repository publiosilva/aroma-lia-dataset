import pytest
from unittest.mock import AsyncMock
from http import HTTPStatus

@pytest.mark.asyncio
async def test_should_get_organization_unit_list_async():
    organization_unit_app_service_mock = AsyncMock()
    organization_unit_app_service_mock.get_list_async.return_value = {
        "Items": [
            {"Name": "test_organizationUnit_1", "Id": "guid-1"},
            {"Name": "test_organizationUnit_2", "Id": "guid-2"},
            {"Name": "test_organizationUnit_3", "Id": "guid-3"},
            {"Name": "test_organizationUnit_4", "Id": "guid-4"},
            {"Name": "test_organizationUnit_5", "Id": "guid-5"}
        ],
        "TotalCount": 10
    }
    organization_units_controller = OrganizationUnitsController(organization_unit_app_service_mock)
    action_result = await organization_units_controller.get_organization_units({})
    assert action_result.status_code == HTTPStatus.OK
    organization_unit_paged_list_result = action_result.json()
    assert organization_unit_paged_list_result["TotalCount"] == 10
    assert len(organization_unit_paged_list_result["Items"]) == 5
