import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;

public class NPathComplexityCheckTest extends AbstractModuleTestSupport {

    @Test
    public void testVisitTokenBeforeExpressionRange() {
        // Create first ast
        final DetailAstImpl astIf = mockAST(TokenTypes.LITERAL_IF, "if", "mockfile", 2, 2);
        final DetailAstImpl astIfLeftParen = mockAST(TokenTypes.LPAREN, "(", "mockfile", 3, 3);
        astIf.addChild(astIfLeftParen);
        final DetailAstImpl astIfTrue =
                mockAST(TokenTypes.LITERAL_TRUE, "true", "mockfile", 3, 3);
        astIf.addChild(astIfTrue);
        final DetailAstImpl astIfRightParen = mockAST(TokenTypes.RPAREN, ")", "mockfile", 4, 4);
        astIf.addChild(astIfRightParen);
        // Create ternary ast
        final DetailAstImpl astTernary = mockAST(TokenTypes.QUESTION, "?", "mockfile", 1, 1);
        final DetailAstImpl astTernaryTrue =
                mockAST(TokenTypes.LITERAL_TRUE, "true", "mockfile", 1, 2);
        astTernary.addChild(astTernaryTrue);

        final NPathComplexityCheck npathComplexityCheckObj = new NPathComplexityCheck();

        // visiting first ast, set expressionSpatialRange to [2,2 - 4,4]
        npathComplexityCheckObj.visitToken(astIf);
        final SortedSet<LocalizedMessage> messages1 = npathComplexityCheckObj.getMessages();

        assertEquals(0, messages1.size(), "No exception messages expected");

        // visiting ternary, it lies before expressionSpatialRange
        npathComplexityCheckObj.visitToken(astTernary);
        final SortedSet<LocalizedMessage> messages2 = npathComplexityCheckObj.getMessages();

        assertEquals(0, messages2.size(), "No exception messages expected");
    }
}
