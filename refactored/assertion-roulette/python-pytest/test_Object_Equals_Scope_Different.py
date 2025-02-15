import pytest

class TestSyntaxTypeReference:

    def test_object_equals_scope_different(self):
        # arrange
        x = TypeReference.create("Foo", TypeContext.Nothing, "a")
        y = TypeReference.create("Foo", TypeContext.Output, "a")
        z = TypeReference.create("Foo", TypeContext.Input)
        # act
        xy = x.equals(y)
        xz = x.equals(z)
        yz = y.equals(z)
        # assert
        assert xy, "Explanation message"
        assert not xz, "Explanation message"
        assert not yz, "Explanation message"
