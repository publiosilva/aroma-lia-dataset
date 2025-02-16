using Xunit;

namespace DefaultNamespace
{
    public class SyntaxTypeReferenceTests
    {
        [Fact]
        public void TypeReference_Equals_Scope_Different1()
        {
            {
                // arrange
                var x = TypeReference.Create("Foo", TypeContext.None, scope: "a");
                var y = TypeReference.Create("Foo", TypeContext.Output, scope: "a");
                var z = TypeReference.Create("Foo", TypeContext.Input);
                // act
                var xy = x.Equals((TypeReference)y);
                var xz = x.Equals((TypeReference)z);
                var yz = y.Equals((TypeReference)z);
                // assert
                Assert.True(xy);
            }
        }

        [Fact]
        public void TypeReference_Equals_Scope_Different2()
        {
            {
                // arrange
                var x = TypeReference.Create("Foo", TypeContext.None, scope: "a");
                var y = TypeReference.Create("Foo", TypeContext.Output, scope: "a");
                var z = TypeReference.Create("Foo", TypeContext.Input);
                // act
                var xy = x.Equals((TypeReference)y);
                var xz = x.Equals((TypeReference)z);
                var yz = y.Equals((TypeReference)z);
                // assert
                Assert.False(xz);
            }
        }

        [Fact]
        public void TypeReference_Equals_Scope_Different3()
        {
            {
                // arrange
                var x = TypeReference.Create("Foo", TypeContext.None, scope: "a");
                var y = TypeReference.Create("Foo", TypeContext.Output, scope: "a");
                var z = TypeReference.Create("Foo", TypeContext.Input);
                // act
                var xy = x.Equals((TypeReference)y);
                var xz = x.Equals((TypeReference)z);
                var yz = y.Equals((TypeReference)z);
                // assert
                Assert.False(yz);
            }
        }
    }
}
