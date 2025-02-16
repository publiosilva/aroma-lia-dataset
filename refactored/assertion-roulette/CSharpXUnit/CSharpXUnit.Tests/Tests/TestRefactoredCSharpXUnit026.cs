using Xunit;

public class SyntaxTypeReferenceTests
{
    [Fact]
    public void TypeReference_Equals_Scope_Different()
    {
        // arrange
        var x = TypeReference.Create("Foo", TypeContext.None, "a");
        var y = TypeReference.Create("Foo", TypeContext.Output, "a");
        var z = TypeReference.Create("Foo", TypeContext.Input);
        // act
        var xy = x.Equals(y);
        var xz = x.Equals(z);
        var yz = y.Equals(z);
        // assert
        Assert.True(xy, "Explanation message");
        Assert.False(xz, "Explanation message");
        Assert.False(yz, "Explanation message");
    }
}
