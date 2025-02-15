import pytest
from unittest.mock import MagicMock
from your_module import UsersController, UserListOutput, PagedListInput, PagedListResult

@pytest.mark.asyncio
async def test_should_get_user_list_async():
    user_app_service_mock = MagicMock()
    user_app_service_mock.get_list_async.return_value = PagedListResult(
        items=[
            UserListOutput(user_name="test_user_1", email="test_user_mail_1@mail", id=guid()),
            UserListOutput(user_name="test_user_2", email="test_user_mail_2@mail", id=guid()),
            UserListOutput(user_name="test_user_3", email="test_user_mail_3@mail", id=guid()),
            UserListOutput(user_name="test_user_4", email="test_user_mail_4@mail", id=guid()),
            UserListOutput(user_name="test_user_5", email="test_user_mail_5@mail", id=guid())
        ],
        total_count=10
    )
    users_controller = UsersController(user_app_service_mock, MagicMock())
    action_result = await users_controller.get_users(PagedListInput())
    
    ok_object_result = assert isinstance(action_result, OkObjectResult)
    user_paged_list_result = assert isinstance(ok_object_result.value, PagedListResult)
    
    assert ok_object_result.status_code == 200
    assert user_paged_list_result.total_count == 10
    assert len(user_paged_list_result.items) == 5
