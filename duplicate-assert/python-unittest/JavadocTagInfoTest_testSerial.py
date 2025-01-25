import unittest

class JavadocTagInfoTest(unittest.TestCase):

    def test_serial(self):
        ast = DetailAstImpl()
        ast_parent = DetailAstImpl()
        ast_parent.set_type(TokenTypes.LITERAL_CATCH)
        set_parent = ast.__class__.__dict__["set_parent"]
        set_parent.__set__(ast, ast_parent)

        valid_types = [
            TokenTypes.VARIABLE_DEF,
        ]
        for type_ in valid_types:
            ast.set_type(type_)
            self.assertTrue(JavadocTagInfo.SERIAL.is_valid_on(ast),
                            f"Invalid ast type for current tag: {ast.get_type()}")

        ast_parent.set_type(TokenTypes.SLIST)
        ast.set_type(TokenTypes.VARIABLE_DEF)
        self.assertFalse(JavadocTagInfo.SERIAL.is_valid_on(ast),
                         "Should return false when ast type is invalid for current tag")

        ast.set_type(TokenTypes.PARAMETER_DEF)
        self.assertFalse(JavadocTagInfo.SERIAL.is_valid_on(ast),
                         "Should return false when ast type is invalid for current tag")
