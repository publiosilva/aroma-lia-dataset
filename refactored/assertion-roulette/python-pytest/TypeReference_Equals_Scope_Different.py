import pytest

class TestSyntaxTypeReference:

    def test_type_reference_equals_scope_different(self):
        # arrange
        x = TypeReference.create("Foo", TypeContext.NONE, "a")
        y = TypeReference.create("Foo", TypeContext.OUTPUT, "a")
        z = TypeReference.create("Foo", TypeContext.INPUT)
        # act
        xy = x.equals(y)
        xz = x.equals(z)
        yz = y.equals(z)
        # assert
        assert xy, "Explanation message"
        assert not xz, "Explanation message"
        assert not yz, "Explanation message"
