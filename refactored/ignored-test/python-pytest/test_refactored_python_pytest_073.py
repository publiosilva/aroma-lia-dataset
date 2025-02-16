import pytest
from unittest import mock

class TestSearch:
    def test_it_calls_search_service_once(self):
        # Arrange
        # Act
        _controller.search("jos")
        # Assert
        mock.speaker_service_mock.assert_called_once_with(mock.ANY)
