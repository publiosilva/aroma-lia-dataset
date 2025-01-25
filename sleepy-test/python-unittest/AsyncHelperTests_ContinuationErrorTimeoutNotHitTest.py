import unittest
import time

class TestAsyncHelper(unittest.TestCase):
    def test_continuation_error_timeout_not_hit(self):
        exceptions = []

        cont = AsyncHelpers.with_timeout(
            AsyncHelpers.prevent_multiple_calls(exceptions.append), 
            0.05
        )

        exception = Exception("Foo")
        cont(exception)
        time.sleep(0.1)

        self.assertEqual(1, len(exceptions))
        self.assertIsNotNone(exceptions[0])
        self.assertIs(exception, exceptions[0])

        cont(None)
        cont(Exception("Some exception"))
        cont(None)
        cont(Exception("Some exception"))

        self.assertEqual(1, len(exceptions))
        self.assertIsNotNone(exceptions[0])
