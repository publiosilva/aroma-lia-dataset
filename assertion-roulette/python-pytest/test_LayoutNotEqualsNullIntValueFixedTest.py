import pytest

class TestLayoutTyped:
    def test_layout_not_equals_null_int_value_fixed(self):
        null_int = None
        layout1 = "2"
        layout2 = null_int
        assert layout1 != null_int
        assert not layout1.equals(null_int)
        assert not layout1.equals(layout2)
        assert layout1 != layout2
        assert hash(layout1) != hash(layout2)
