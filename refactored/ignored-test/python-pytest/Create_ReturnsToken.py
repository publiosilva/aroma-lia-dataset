import pytest
from unittest.mock import Mock

class TestTokenController:

    # @pytest.mark.skip
    def test_create_returns_token(self):
        vm = TokenViewModel()
        vm.set_email("admin@contoso.com")
        vm.set_password("Pass@word1!")
        result = _sut.create(vm)
        assert isinstance(result, OkObjectResult)
        token = result.get_value()
        assert "token" in token
