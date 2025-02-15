import pytest
import httpx

@pytest.mark.skip(reason="")
@pytest.mark.asyncio
async def test_department_api_returns_array_of_department_objects():
    url = "/Departments"
    async with httpx.AsyncClient() as client:
        response = await client.get(url)
        assert response.status_code == 200
        content = response.text
        assert "English" in content
