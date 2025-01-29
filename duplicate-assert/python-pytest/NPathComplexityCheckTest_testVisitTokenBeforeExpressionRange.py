def test_visit_token_before_expression_range():
    ast_if = mockAST(TokenTypes.LITERAL_IF, "if", "mockfile", 2, 2)
    ast_if_left_paren = mockAST(TokenTypes.LPAREN, "(", "mockfile", 3, 3)
    ast_if.addChild(ast_if_left_paren)
    ast_if_true = mockAST(TokenTypes.LITERAL_TRUE, "true", "mockfile", 3, 3)
    ast_if.addChild(ast_if_true)
    ast_if_right_paren = mockAST(TokenTypes.RPAREN, ")", "mockfile", 4, 4)
    ast_if.addChild(ast_if_right_paren)

    ast_ternary = mockAST(TokenTypes.QUESTION, "?", "mockfile", 1, 1)
    ast_ternary_true = mockAST(TokenTypes.LITERAL_TRUE, "true", "mockfile", 1, 2)
    ast_ternary.addChild(ast_ternary_true)

    npath_complexity_check_obj = NPathComplexityCheck()

    npath_complexity_check_obj.visitToken(ast_if)
    messages1 = npath_complexity_check_obj.getMessages()

    assert len(messages1) == 0, "No exception messages expected"

    npath_complexity_check_obj.visitToken(ast_ternary)
    messages2 = npath_complexity_check_obj.getMessages()

    assert len(messages2) == 0, "No exception messages expected"
