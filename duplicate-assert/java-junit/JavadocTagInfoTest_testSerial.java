import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.Test;

public class JavadocTagInfoTest {

    @Test
    public void testSerial() throws ReflectiveOperationException {
        final DetailAstImpl ast = new DetailAstImpl();
        final DetailAstImpl astParent = new DetailAstImpl();
        astParent.setType(TokenTypes.LITERAL_CATCH);
        final Method setParent = ast.getClass().getDeclaredMethod("setParent", DetailAstImpl.class);
        setParent.setAccessible(true);
        setParent.invoke(ast, astParent);

        final int[] validTypes = {
            TokenTypes.VARIABLE_DEF,
        };
        for (int type: validTypes) {
            ast.setType(type);
            assertTrue(JavadocTagInfo.SERIAL.isValidOn(ast),
                    "Invalid ast type for current tag: " + ast.getType());
        }

        astParent.setType(TokenTypes.SLIST);
        ast.setType(TokenTypes.VARIABLE_DEF);
        assertFalse(JavadocTagInfo.SERIAL.isValidOn(ast),
                "Should return false when ast type is invalid for current tag");

        ast.setType(TokenTypes.PARAMETER_DEF);
        assertFalse(JavadocTagInfo.SERIAL.isValidOn(ast),
                "Should return false when ast type is invalid for current tag");
    }
}
