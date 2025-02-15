import pytest
from unittest.mock import Mock

@pytest.mark.skip(reason="")
class TestSearch:
    def test_it_calls_search_service_once(self):
        # Arrange
        _controller = Mock()
        _speaker_service_mock = Mock()
        
        # Act
        _controller.Search("jos")
        
        # Assert
        _speaker_service_mock.Search.assert_called_once_with(Mock())
