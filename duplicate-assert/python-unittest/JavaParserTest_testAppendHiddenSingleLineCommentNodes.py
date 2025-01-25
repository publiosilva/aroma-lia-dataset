import unittest

class JavaParserTest(unittest.TestCase):

    def test_append_hidden_single_line_comment_nodes(self):
        root = JavaParser.parse_file(File(get_path("InputJavaParserHiddenComments.java")),
                                     JavaParser.Options.WITH_COMMENTS)

        single_line_comment = TestUtil.find_token_in_ast_by_predicate(
            root, lambda ast: ast.get_type() == TokenTypes.SINGLE_LINE_COMMENT
        )
        self.assertTrue(single_line_comment.is_present(), "Single line comment should be present")

        comment = single_line_comment.get()

        self.assertEqual(13, comment.get_line_no(), "Unexpected line number")
        self.assertEqual(0, comment.get_column_no(), "Unexpected column number")
        self.assertEqual("//", comment.get_text(), "Unexpected comment content")

        comment_content = comment.get_first_child()

        self.assertEqual(TokenTypes.COMMENT_CONTENT, comment_content.get_type(), "Unexpected token type")
        self.assertEqual(13, comment_content.get_line_no(), "Unexpected line number")
        self.assertEqual(2, comment_content.get_column_no(), "Unexpected column number")
        self.assertTrue(comment_content.get_text().startswith(" inline comment"),
                        "Unexpected comment content")
