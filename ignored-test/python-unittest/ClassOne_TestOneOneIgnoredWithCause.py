import unittest

class TestClassOne(unittest.TestCase):
    @unittest.skip("This is the ignore cause")
    def test_one_one_ignored_with_cause(self):
        self.assertTrue(1 == 2, "This is ignored with cause, no failure")
