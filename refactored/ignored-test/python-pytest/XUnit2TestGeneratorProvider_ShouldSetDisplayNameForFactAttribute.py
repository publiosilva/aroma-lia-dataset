import pytest

class TestXUnit2TestGeneratorProvider:

    # @pytest.mark.skip(reason="")

    def test_xunit2_test_generator_provider_should_set_display_name_for_fact_attribute(self):
        # Arrange
        provider = XUnit2TestGeneratorProvider(CodeDomHelper(CSharpCodeProvider()))
        context = Generator.TestClassGenerationContext(
            None, 
            Parser.SpecFlowDocument(SpecFlowFeature(None, None, None, None, "", None, None), None, None), 
            None, None, None, None, None, None, None, None, False
        )
        code_member_method = CodeMemberMethod()
        
        # Act
        provider.SetTestMethod(context, code_member_method, "Foo")
        
        # Assert
        modified_attribute = next((a for a in code_member_method.getCustomAttributes() if a.getName() == "Xunit.SkippableFactAttribute"), None)
        assert modified_attribute is not None
        
        attribute = next((a for a in modified_attribute.getArguments() if a.getName() == "DisplayName"), None)
        assert attribute is not None
        
        primitive_expression = attribute.getValue()
        assert isinstance(primitive_expression, CodePrimitiveExpression)
        assert primitive_expression.getValue() == "Foo"
