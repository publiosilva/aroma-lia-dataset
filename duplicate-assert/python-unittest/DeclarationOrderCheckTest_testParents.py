import unittest

class DeclarationOrderCheckTest(unittest.TestCase):

    def test_parents(self):
        parent = DetailAstImpl()
        parent.set_type(TokenTypes.STATIC_INIT)
        method = DetailAstImpl()
        method.set_type(TokenTypes.METHOD_DEF)
        parent.set_first_child(method)
        ctor = DetailAstImpl()
        ctor.set_type(TokenTypes.CTOR_DEF)
        method.set_next_sibling(ctor)

        check = DeclarationOrderCheck()

        check.visit_token(method)
        messages1 = check.get_messages()

        self.assertEqual(0, len(messages1), "No exception messages expected")

        check.visit_token(ctor)
        messages2 = check.get_messages()

        self.assertEqual(0, len(messages2), "No exception messages expected")
