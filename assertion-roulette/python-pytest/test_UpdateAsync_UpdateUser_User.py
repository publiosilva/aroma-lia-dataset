import pytest
from unittest.mock import Mock

class TestEditUserSuccess:
    @pytest.mark.asyncio
    async def test_update_async_update_user_user(self):
        # Arrange
        getable_user = Mock()
        user_repository = Mock()
        edit_user = EditUser(getable_user, user_repository)
        getable_user.get_by_id.return_value = _user
        _user.name = "TestName"
        # Act
        result = await edit_user.update_async(_user)
        # Assert
        assert result is not None
        assert result == _user
        assert result.id == _user.id
        assert result.name == _user.name
