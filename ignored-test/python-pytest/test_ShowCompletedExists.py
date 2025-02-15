import pytest

class TestToDoApplication:
    @pytest.mark.skip(reason="")
    def test_show_completed_exists(self):
        # Arrange
        todo = TodoList()
        # Act
        todo.show_completed = True
