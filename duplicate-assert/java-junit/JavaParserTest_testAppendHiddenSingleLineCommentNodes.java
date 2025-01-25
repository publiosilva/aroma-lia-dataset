import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.Test;

public class JavaParserTest extends AbstractModuleTestSupport {

    @Test
    public void testAppendHiddenSingleLineCommentNodes() throws Exception {
        final DetailAST root =
            JavaParser.parseFile(new File(getPath("InputJavaParserHiddenComments.java")),
                JavaParser.Options.WITH_COMMENTS);

        final Optional<DetailAST> singleLineComment = TestUtil.findTokenInAstByPredicate(root,
            ast -> ast.getType() == TokenTypes.SINGLE_LINE_COMMENT);
        assertTrue(singleLineComment.isPresent(), "Single line comment should be present");

        final DetailAST comment = singleLineComment.get();

        assertEquals(13, comment.getLineNo(), "Unexpected line number");
        assertEquals(0, comment.getColumnNo(), "Unexpected column number");
        assertEquals("//", comment.getText(), "Unexpected comment content");

        final DetailAST commentContent = comment.getFirstChild();

        assertEquals(TokenTypes.COMMENT_CONTENT, commentContent.getType(), "Unexpected token type");
        assertEquals(13, commentContent.getLineNo(), "Unexpected line number");
        assertEquals(2, commentContent.getColumnNo(), "Unexpected column number");
        assertTrue(commentContent.getText().startsWith(" inline comment"),
                "Unexpected comment content");
    }
}
