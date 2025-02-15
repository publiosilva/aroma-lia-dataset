import pytest
from unittest.mock import MagicMock

def test_update_async_update_user_user():
    # Arrange
    getable_user = MagicMock()
    user_repository = MagicMock()
    edit_user = EditUser(getable_user, user_repository)
    getable_user.get_by_id.return_value = _user
    _user.name = "TestName"
    
    # Act
    result = edit_user.update_async(_user)
    
    # Assert
    assert result is not None, "Explanation message"
    assert result == _user, "Explanation message"
    assert result.id == _user.id, "Explanation message"
    assert result.name == _user.name, "Explanation message"
