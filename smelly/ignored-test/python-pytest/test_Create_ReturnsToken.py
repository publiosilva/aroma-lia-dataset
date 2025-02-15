import pytest

@pytest.mark.skip(reason="")
class TestTokenController:
    async def test_create_returns_token(self):
        vm = TokenViewModel(
            Email="admin@contoso.com",
            Password="Pass@word1!"
        )
        result = await self._sut.create(vm)
        assert isinstance(result, OkObjectResult)
        token = str(result.value)
        assert "token" in token
