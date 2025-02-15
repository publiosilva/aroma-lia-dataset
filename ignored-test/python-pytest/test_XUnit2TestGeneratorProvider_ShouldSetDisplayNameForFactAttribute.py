import pytest

class TestXUnit2TestGeneratorProvider:

    @pytest.mark.skip(reason="")
    def test_xunit2_test_generator_provider_should_set_display_name_for_fact_attribute(self):
        # Arrange
        provider = XUnit2TestGeneratorProvider(CodeDomHelper(CSharpCodeProvider()))
        context = Generator.TestClassGenerationContext(unitTestGeneratorProvider=None, document=Parser.SpecFlowDocument(feature=SpecFlowFeature(tags=None, location=None, language=None, keyword=None, name="", description=None, children=None), comments=None, documentLocation=None), ns=None, testClass=None, testRunnerField=None, testClassInitializeMethod=None, testClassCleanupMethod=None, testInitializeMethod=None, testCleanupMethod=None, scenarioInitializeMethod=None, scenarioStartMethod=None, scenarioCleanupMethod=None, featureBackgroundMethod=None, generateRowTests=False)
        code_member_method = CodeMemberMethod()
        # Act
        provider.SetTestMethod(context, code_member_method, "Foo")
        # Assert
        modified_attribute = next((a for a in code_member_method.CustomAttributes if a.Name == "Xunit.SkippableFactAttribute"), None)
        assert modified_attribute is not None
        attribute = next((a for a in modified_attribute.Arguments if a.Name == "DisplayName"), None)
        assert attribute is not None
        primitive_expression = attribute.Value
        assert isinstance(primitive_expression, CodePrimitiveExpression)
        assert primitive_expression.Value == "Foo"
