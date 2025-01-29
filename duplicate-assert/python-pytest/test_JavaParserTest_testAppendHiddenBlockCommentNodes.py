def test_append_hidden_block_comment_nodes():
    root = JavaParser.parseFile(
        File(getPath("InputJavaParserHiddenComments.java")),
        JavaParser.Options.WITH_COMMENTS
    )

    block_comment = TestUtil.findTokenInAstByPredicate(
        root, lambda ast: ast.getType() == TokenTypes.BLOCK_COMMENT_BEGIN
    )

    assert block_comment.isPresent(), "Block comment should be present"

    comment = block_comment.get()

    assert comment.getLineNo() == 3, "Unexpected line number"
    assert comment.getColumnNo() == 0, "Unexpected column number"
    assert comment.getText() == "/*", "Unexpected comment content"

    comment_content = comment.getFirstChild()
    comment_end = comment.getLastChild()

    assert comment_content.getLineNo() == 3, "Unexpected line number"
    assert comment_content.getColumnNo() == 2, "Unexpected column number"
    assert comment_end.getLineNo() == 9, "Unexpected line number"
    assert comment_end.getColumnNo() == 1, "Unexpected column number"
