using Xunit;

public class XUnit2TestGeneratorProviderTests
{
    // [Fact(Skip = "")]

    [Fact]
    public void XUnit2TestGeneratorProvider_ShouldSetDisplayNameForFactAttribute()
    {
        // Arrange
        var provider = new XUnit2TestGeneratorProvider(new CodeDomHelper(new CSharpCodeProvider()));
        var context = new Generator.TestClassGenerationContext(null, new Parser.SpecFlowDocument(new SpecFlowFeature(null, null, null, null, "", null, null), null, null), null, null, null, null, null, null, null, null, null, false);
        var codeMemberMethod = new CodeMemberMethod();
        // Act
        provider.SetTestMethod(context, codeMemberMethod, "Foo");
        // Assert
        var modifiedAttribute = codeMemberMethod.CustomAttributes.FirstOrDefault(a => a.Name == "Xunit.SkippableFactAttribute");
        Assert.NotNull(modifiedAttribute);
        var attribute = modifiedAttribute.Arguments.FirstOrDefault(a => a.Name == "DisplayName");
        Assert.NotNull(attribute);
        var primitiveExpression = attribute.Value as CodePrimitiveExpression;
        Assert.NotNull(primitiveExpression);
        Assert.Equal("Foo", primitiveExpression.Value);
    }
}
