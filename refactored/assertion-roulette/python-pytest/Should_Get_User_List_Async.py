import pytest
from unittest.mock import MagicMock

class TestUsersController:

    @pytest.mark.asyncio
    async def test_should_get_user_list_async(self):
        user_app_service_mock = MagicMock()
        user_app_service_mock.get_list_async.return_value = asyncio.Future()
        user_app_service_mock.get_list_async.return_value.set_result(PagedListResult(
            items=[
                UserListOutput("test_user_1", "test_user_mail_1@mail", uuid.uuid4()),
                UserListOutput("test_user_2", "test_user_mail_2@mail", uuid.uuid4()),
                UserListOutput("test_user_3", "test_user_mail_3@mail", uuid.uuid4()),
                UserListOutput("test_user_4", "test_user_mail_4@mail", uuid.uuid4()),
                UserListOutput("test_user_5", "test_user_mail_5@mail", uuid.uuid4())
            ],
            total_count=10
        ))

        users_controller = UsersController(user_app_service_mock, MagicMock())
        action_result = await users_controller.get_users(PagedListInput())

        assert isinstance(action_result, OkObjectResult), "Explanation message"
        ok_object_result = action_result
        assert isinstance(ok_object_result.value, PagedListResult), "Explanation message"
        user_paged_list_result = ok_object_result.value
        assert ok_object_result.status_code == HttpStatus.OK.value(), "Explanation message"
        assert user_paged_list_result.total_count == 10, "Explanation message"
        assert len(user_paged_list_result.items) == 5, "Explanation message"
