import pytest

class TestMyFluentWrapper:

    @pytest.mark.skip(reason="")
    def test_call_site_should_work_for_async_methods_with_return_value(self):
        call_site = get_async_call_site().result()
        assert call_site == "NLog.UnitTests.LayoutRenderers.CallSiteTests.GetAsyncCallSite"
