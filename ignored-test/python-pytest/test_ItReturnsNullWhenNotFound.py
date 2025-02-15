import pytest

class TestGet:
    @pytest.mark.skipif(True, reason="")
    def test_it_returns_null_when_not_found(self):
        # Arrange
        # Act
        result = self._repo.get(-1)
        # Assert
        assert result is None
