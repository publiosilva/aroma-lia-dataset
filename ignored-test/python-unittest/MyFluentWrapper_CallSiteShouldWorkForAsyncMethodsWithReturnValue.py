import unittest

class TestMyFluentWrapper(unittest.TestCase):
    @unittest.skip("")
    def test_call_site_should_work_for_async_methods_with_return_value(self):
        call_site = get_async_call_site()
        self.assertEqual("NLog.UnitTests.LayoutRenderers.CallSiteTests.GetAsyncCallSite", call_site)
