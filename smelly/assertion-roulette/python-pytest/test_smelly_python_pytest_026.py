import pytest

class TestSyntaxTypeReference:
    def test_type_reference_equals_scope_different(self):
        # arrange
        x = TypeReference.create("Foo", TypeContext.none, scope="a")
        y = TypeReference.create("Foo", TypeContext.output, scope="a")
        z = TypeReference.create("Foo", TypeContext.input)
        # act
        xy = x.equals(y)
        xz = x.equals(z)
        yz = y.equals(z)
        # assert
        assert xy is True
        assert xz is False
        assert yz is False
