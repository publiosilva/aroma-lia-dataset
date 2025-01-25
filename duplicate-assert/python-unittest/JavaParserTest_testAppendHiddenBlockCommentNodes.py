import unittest

class JavaParserTest(unittest.TestCase):

    def test_append_hidden_block_comment_nodes(self):
        root = JavaParser.parse_file(File(get_path("InputJavaParserHiddenComments.java")),
                                     JavaParser.Options.WITH_COMMENTS)

        block_comment = TestUtil.find_token_in_ast_by_predicate(
            root, lambda ast: ast.get_type() == TokenTypes.BLOCK_COMMENT_BEGIN
        )

        self.assertTrue(block_comment.is_present(), "Block comment should be present")

        comment = block_comment.get()

        self.assertEqual(3, comment.get_line_no(), "Unexpected line number")
        self.assertEqual(0, comment.get_column_no(), "Unexpected column number")
        self.assertEqual("/*", comment.get_text(), "Unexpected comment content")

        comment_content = comment.get_first_child()
        comment_end = comment.get_last_child()

        self.assertEqual(3, comment_content.get_line_no(), "Unexpected line number")
        self.assertEqual(2, comment_content.get_column_no(), "Unexpected column number")
        self.assertEqual(9, comment_end.get_line_no(), "Unexpected line number")
        self.assertEqual(1, comment_end.get_column_no(), "Unexpected column number")
