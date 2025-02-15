import pytest

class TestGet:
    # @pytest.mark.skip(reason="")
    def test_it_returns_null_when_not_found(self):
        # Arrange
        # Act
        result = _repo.get(-1)
        # Assert
        assert result is None
