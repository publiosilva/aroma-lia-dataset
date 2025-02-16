using Xunit;

namespace DefaultNamespace
{
    public class SyntaxTypeReferenceTests
    {
        [Fact]
        public void Object_Equals_Scope_Different()
        {
            {
                // arrange
                var x = TypeReference.Create("Foo", TypeContext.None, scope: "a");
                var y = TypeReference.Create("Foo", TypeContext.Output, scope: "a");
                var z = TypeReference.Create("Foo", TypeContext.Input);
                // act
                var xy = x.Equals((object)y);
                var xz = x.Equals((object)z);
                var yz = y.Equals((object)z);
                // assert
                Assert.True(xy);
                Assert.False(xz);
                Assert.False(yz);
            }
        }
    }
}
