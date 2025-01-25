import unittest

class TestClassOne(unittest.TestCase):
    @unittest.skip("")
    def test_one_one_ignored(self):
        self.assertTrue(1 == 2, "This is ignored, no failure")
