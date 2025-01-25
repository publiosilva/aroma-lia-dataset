import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.Test;

public class JavaParserTest extends AbstractModuleTestSupport {

    @Test
    public void testAppendHiddenBlockCommentNodes() throws Exception {
        final DetailAST root =
            JavaParser.parseFile(new File(getPath("InputJavaParserHiddenComments.java")),
                JavaParser.Options.WITH_COMMENTS);

        final Optional<DetailAST> blockComment = TestUtil.findTokenInAstByPredicate(root,
            ast -> ast.getType() == TokenTypes.BLOCK_COMMENT_BEGIN);

        assertTrue(blockComment.isPresent(), "Block comment should be present");

        final DetailAST comment = blockComment.get();

        assertEquals(3, comment.getLineNo(), "Unexpected line number");
        assertEquals(0, comment.getColumnNo(), "Unexpected column number");
        assertEquals("/*", comment.getText(), "Unexpected comment content");

        final DetailAST commentContent = comment.getFirstChild();
        final DetailAST commentEnd = comment.getLastChild();

        assertEquals(3, commentContent.getLineNo(), "Unexpected line number");
        assertEquals(2, commentContent.getColumnNo(), "Unexpected column number");
        assertEquals(9, commentEnd.getLineNo(), "Unexpected line number");
        assertEquals(1, commentEnd.getColumnNo(), "Unexpected column number");
    }
}
