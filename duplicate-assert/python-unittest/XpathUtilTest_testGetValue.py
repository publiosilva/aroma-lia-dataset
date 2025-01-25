import unittest

class XpathUtilTest(unittest.TestCase):

    def test_get_value(self):
        self.assertEqual("HELLO WORLD", get_text_attribute_value(
            create_detail_ast(TokenTypes.STRING_LITERAL, "\"HELLO WORLD\"")),
            "Returned value differs from expected")
        self.assertEqual("123", get_text_attribute_value(
            create_detail_ast(TokenTypes.NUM_INT, "123")),
            "Returned value differs from expected")
        self.assertEqual("HELLO WORLD", get_text_attribute_value(
            create_detail_ast(TokenTypes.IDENT, "HELLO WORLD")),
            "Returned value differs from expected")
        self.assertNotEqual("HELLO WORLD", get_text_attribute_value(
            create_detail_ast(TokenTypes.STRING_LITERAL, "HELLO WORLD")),
            "Returned value differs from expected")
