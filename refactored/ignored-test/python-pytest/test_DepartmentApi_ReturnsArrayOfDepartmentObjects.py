import pytest

class TestApiIntegration:

    # @pytest.mark.skipif(True, reason="Condition for skipping the test")
    def test_department_api_returns_array_of_department_objects(self):
        url = "/Departments"
        with init_test_server() as th:
            client = th.create_client()
            response = client.get_async(url).join()
            assert response.get_status_code() == HttpStatusCode.OK
            content = response.get_content().read_as_string_async().join()
            assert "English" in content
