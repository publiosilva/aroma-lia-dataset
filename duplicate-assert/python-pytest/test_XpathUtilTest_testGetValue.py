def test_get_value():
    assert getTextAttributeValue(
        createDetailAST(TokenTypes.STRING_LITERAL, "\"HELLO WORLD\"")
    ) == "HELLO WORLD", "Returned value differs from expected"

    assert getTextAttributeValue(
        createDetailAST(TokenTypes.NUM_INT, "123")
    ) == "123", "Returned value differs from expected"

    assert getTextAttributeValue(
        createDetailAST(TokenTypes.IDENT, "HELLO WORLD")
    ) == "HELLO WORLD", "Returned value differs from expected"

    assert getTextAttributeValue(
        createDetailAST(TokenTypes.STRING_LITERAL, "HELLO WORLD")
    ) != "HELLO WORLD", "Returned value differs from expected"
