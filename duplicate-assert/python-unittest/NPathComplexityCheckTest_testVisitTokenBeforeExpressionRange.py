import unittest

class NPathComplexityCheckTest(unittest.TestCase):

    def test_visit_token_before_expression_range(self):
        ast_if = mock_ast(TokenTypes.LITERAL_IF, "if", "mockfile", 2, 2)
        ast_if_left_paren = mock_ast(TokenTypes.LPAREN, "(", "mockfile", 3, 3)
        ast_if.add_child(ast_if_left_paren)
        ast_if_true = mock_ast(TokenTypes.LITERAL_TRUE, "true", "mockfile", 3, 3)
        ast_if.add_child(ast_if_true)
        ast_if_right_paren = mock_ast(TokenTypes.RPAREN, ")", "mockfile", 4, 4)
        ast_if.add_child(ast_if_right_paren)

        ast_ternary = mock_ast(TokenTypes.QUESTION, "?", "mockfile", 1, 1)
        ast_ternary_true = mock_ast(TokenTypes.LITERAL_TRUE, "true", "mockfile", 1, 2)
        ast_ternary.add_child(ast_ternary_true)

        npath_complexity_check_obj = NPathComplexityCheck()

        npath_complexity_check_obj.visit_token(ast_if)
        messages1 = npath_complexity_check_obj.get_messages()

        self.assertEqual(0, len(messages1), "No exception messages expected")

        npath_complexity_check_obj.visit_token(ast_ternary)
        messages2 = npath_complexity_check_obj.get_messages()

        self.assertEqual(0, len(messages2), "No exception messages expected")
