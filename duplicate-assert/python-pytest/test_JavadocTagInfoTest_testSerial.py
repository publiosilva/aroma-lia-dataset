def test_serial():
    ast = DetailAstImpl()
    ast_parent = DetailAstImpl()
    ast_parent.setType(TokenTypes.LITERAL_CATCH)
    set_parent = ast.__class__.getDeclaredMethod("setParent", DetailAstImpl)
    set_parent.setAccessible(True)
    set_parent.invoke(ast, ast_parent)

    valid_types = [
        TokenTypes.VARIABLE_DEF,
    ]
    for type_ in valid_types:
        ast.setType(type_)
        assert JavadocTagInfo.SERIAL.isValidOn(ast), f"Invalid ast type for current tag: {ast.getType()}"

    ast_parent.setType(TokenTypes.SLIST)
    ast.setType(TokenTypes.VARIABLE_DEF)
    assert not JavadocTagInfo.SERIAL.isValidOn(ast), "Should return false when ast type is invalid for current tag"

    ast.setType(TokenTypes.PARAMETER_DEF)
    assert not JavadocTagInfo.SERIAL.isValidOn(ast), "Should return false when ast type is invalid for current tag"
