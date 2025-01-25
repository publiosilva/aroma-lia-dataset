import unittest

class TestEnv(unittest.TestCase):
    @unittest.skip("")
    def test_get_value_when_calls_not_implemented(self):
        raise NotImplementedError()
