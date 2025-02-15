import org.junit.Test;
import org.junit.Ignore;

import static org.junit.Assert.*;

public class XUnit2TestGeneratorProviderTests {

    @Ignore("")

    @Test
    public void XUnit2TestGeneratorProvider_ShouldSetDisplayNameForFactAttribute() {
        {
            // Arrange
            XUnit2TestGeneratorProvider provider = new XUnit2TestGeneratorProvider(new CodeDomHelper(new CSharpCodeProvider()));
            Generator.TestClassGenerationContext context = new Generator.TestClassGenerationContext(null, new Parser.SpecFlowDocument(new SpecFlowFeature(null, null, null, null, "", null, null), null, null), null, null, null, null, null, null, null, null, null, false);
            CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
            // Act
            provider.SetTestMethod(context, codeMemberMethod, "Foo");
            // Assert
            CodeAttributeDeclaration modifiedAttribute = codeMemberMethod.getCustomAttributes().stream().filter(a -> a.getName().equals("Xunit.SkippableFactAttribute")).findFirst().orElse(null);
            assertNotNull(modifiedAttribute);
            CodeAttributeArgument attribute = modifiedAttribute.getArguments().stream().filter(a -> a.getName().equals("DisplayName")).findFirst().orElse(null);
            assertNotNull(attribute);
            CodePrimitiveExpression primitiveExpression = (CodePrimitiveExpression) attribute.getValue();
            assertNotNull(primitiveExpression);
            assertEquals("Foo", primitiveExpression.getValue());
        }
    }
}
