import unittest
import time

class TestAsyncHelper(unittest.TestCase):
    def test_continuation_timeout_not_hit(self):
        exceptions = []

        cont = AsyncHelpers.with_timeout(
            AsyncHelpers.prevent_multiple_calls(exceptions.append),
            0.05
        )

        cont(None)
        time.sleep(0.1)

        self.assertEqual(1, len(exceptions))
        self.assertIsNone(exceptions[0])

        cont(None)
        cont(Exception("Some exception"))
        cont(None)
        cont(Exception("Some exception"))

        self.assertEqual(1, len(exceptions))
        self.assertIsNone(exceptions[0])
