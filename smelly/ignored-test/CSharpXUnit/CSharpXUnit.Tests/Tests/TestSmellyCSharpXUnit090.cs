using Xunit;

namespace DefaultNamespace
{
    public class XUnit2TestGeneratorProviderTests
    {
        [Fact(Skip = "")]
        public void XUnit2TestGeneratorProvider_ShouldSetDisplayNameForFactAttribute()
        {
            {
                // Arrange
                var provider = new XUnit2TestGeneratorProvider(new CodeDomHelper(new CSharpCodeProvider()));
                var context = new Generator.TestClassGenerationContext(unitTestGeneratorProvider: null, document: new Parser.SpecFlowDocument(feature: new SpecFlowFeature(tags: null, location: null, language: null, keyword: null, name: "", description: null, children: null), comments: null, documentLocation: null), ns: null, testClass: null, testRunnerField: null, testClassInitializeMethod: null, testClassCleanupMethod: null, testInitializeMethod: null, testCleanupMethod: null, scenarioInitializeMethod: null, scenarioStartMethod: null, scenarioCleanupMethod: null, featureBackgroundMethod: null, generateRowTests: false);
                var codeMemberMethod = new CodeMemberMethod();
                // Act
                provider.SetTestMethod(context, codeMemberMethod, "Foo");
                // Assert
                var modifiedAttribute = codeMemberMethod.CustomAttributes.OfType<CodeAttributeDeclaration>().FirstOrDefault(a => a.Name == "Xunit.SkippableFactAttribute");
                modifiedAttribute.Should().NotBeNull();
                var attribute = modifiedAttribute.Arguments.OfType<CodeAttributeArgument>().FirstOrDefault(a => a.Name == "DisplayName");
                attribute.Should().NotBeNull();
                var primitiveExpression = attribute.Value as CodePrimitiveExpression;
                primitiveExpression.Should().NotBeNull();
                primitiveExpression.Value.Should().Be("Foo");
            }
        }
    }
}
