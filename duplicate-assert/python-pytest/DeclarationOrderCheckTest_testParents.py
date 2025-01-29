def test_parents():
    parent = DetailAstImpl()
    parent.setType(TokenTypes.STATIC_INIT)
    method = DetailAstImpl()
    method.setType(TokenTypes.METHOD_DEF)
    parent.setFirstChild(method)
    ctor = DetailAstImpl()
    ctor.setType(TokenTypes.CTOR_DEF)
    method.setNextSibling(ctor)

    check = DeclarationOrderCheck()

    check.visitToken(method)
    messages1 = check.getMessages()

    assert len(messages1) == 0, "No exception messages expected"

    check.visitToken(ctor)
    messages2 = check.getMessages()

    assert len(messages2) == 0, "No exception messages expected"
