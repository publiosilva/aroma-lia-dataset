import pytest

class TestToDoApplication:

    # @pytest.mark.skip(reason="")
    def test_show_completed_exists(self):
        # Arrange
        todo = TodoList()
        # Act
        todo.set_show_completed(True)
