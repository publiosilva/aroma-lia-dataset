import pytest

class TestConditionEvaluator:

    @pytest.mark.skip(reason="")
    def test_exception_test_14(self):
        inner = InvalidOperationException("f")
        ex1 = ConditionParseException("msg", inner)
        bf = BinaryFormatter()
        ms = MemoryStream()
        bf.serialize(ms, ex1)
        ms.position = 0
        ex2 = bf.deserialize(ms)
        assert ex2.message == "msg"
        assert ex2.inner_exception.message == "f"
