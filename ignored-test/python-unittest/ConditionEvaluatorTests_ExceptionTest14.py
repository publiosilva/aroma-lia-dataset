import unittest
import pickle
from io import BytesIO

class TestConditionEvaluator(unittest.TestCase):
    @unittest.skip("")
    def test_exception_test_14(self):
        inner = ValueError("f")
        ex1 = ConditionParseException("msg", inner)
        buffer = BytesIO()
        pickle.dump(ex1, buffer)
        buffer.seek(0)
        ex2 = pickle.load(buffer)
        self.assertEqual("msg", str(ex2))
        self.assertEqual("f", str(ex2.__cause__))
