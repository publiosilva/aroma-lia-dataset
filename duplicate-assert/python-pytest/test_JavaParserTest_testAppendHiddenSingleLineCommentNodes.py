def test_append_hidden_single_line_comment_nodes():
    root = JavaParser.parseFile(
        File(getPath("InputJavaParserHiddenComments.java")),
        JavaParser.Options.WITH_COMMENTS
    )

    single_line_comment = TestUtil.findTokenInAstByPredicate(
        root, lambda ast: ast.getType() == TokenTypes.SINGLE_LINE_COMMENT
    )
    assert single_line_comment.isPresent(), "Single line comment should be present"

    comment = single_line_comment.get()

    assert comment.getLineNo() == 13, "Unexpected line number"
    assert comment.getColumnNo() == 0, "Unexpected column number"
    assert comment.getText() == "//", "Unexpected comment content"

    comment_content = comment.getFirstChild()

    assert comment_content.getType() == TokenTypes.COMMENT_CONTENT, "Unexpected token type"
    assert comment_content.getLineNo() == 13, "Unexpected line number"
    assert comment_content.getColumnNo() == 2, "Unexpected column number"
    assert comment_content.getText().startswith(" inline comment"), "Unexpected comment content"
