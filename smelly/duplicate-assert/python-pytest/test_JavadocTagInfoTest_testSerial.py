import pytest

class TestJavadocTagInfo:
    def test_serial(self):
        ast = DetailAstImpl()
        ast_parent = DetailAstImpl()
        ast_parent.set_type(TokenTypes.LiteralCatch)
        set_parent = ast.__class__.__dict__['set_parent']
        set_parent(ast, ast_parent)

        valid_types = [TokenTypes.VariableDef]
        for type in valid_types:
            ast.set_type(type)
            assert JavadocTagInfo.Serial.is_valid_on(ast), f"Invalid ast type for current tag: {type}"

        ast_parent.set_type(TokenTypes.Slist)
        ast.set_type(TokenTypes.VariableDef)
        assert not JavadocTagInfo.Serial.is_valid_on(ast), "Should return false when ast type is invalid for current tag"

        ast.set_type(TokenTypes.ParameterDef)
        assert not JavadocTagInfo.Serial.is_valid_on(ast), "Should return false when ast type is invalid for current tag"
