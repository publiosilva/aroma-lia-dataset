import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class JavadocTagInfoTest {

    @Test
    public void testSerial1() {
        DetailAstImpl ast = new DetailAstImpl();
        DetailAstImpl astParent = new DetailAstImpl();
        astParent.setType(TokenTypes.LiteralCatch);
        var setParent = ast.getClass().getDeclaredMethod("setParent", DetailAstImpl.class);
        setParent.setAccessible(true);
        setParent.invoke(ast, astParent);

        TokenTypes[] validTypes = new TokenTypes[]{TokenTypes.VariableDef};
        for (TokenTypes type : validTypes) {
            ast.setType(type);
            assertTrue("Invalid ast type for current tag: " + ast.getClass(), JavadocTagInfo.Serial.isValidOn(ast));
        }

        astParent.setType(TokenTypes.Slist);
        ast.setType(TokenTypes.VariableDef);
        assertFalse("Should return false when ast type is invalid for current tag", JavadocTagInfo.Serial.isValidOn(ast));
    }

    @Test
    public void testSerial2() {
        DetailAstImpl ast = new DetailAstImpl();
        DetailAstImpl astParent = new DetailAstImpl();
        astParent.setType(TokenTypes.LiteralCatch);
        var setParent = ast.getClass().getDeclaredMethod("setParent", DetailAstImpl.class);
        setParent.setAccessible(true);
        setParent.invoke(ast, astParent);

        TokenTypes[] validTypes = new TokenTypes[]{TokenTypes.VariableDef};
        for (TokenTypes type : validTypes) {
            ast.setType(type);
            assertTrue("Invalid ast type for current tag: " + ast.getClass(), JavadocTagInfo.Serial.isValidOn(ast));
        }

        astParent.setType(TokenTypes.Slist);
        ast.setType(TokenTypes.VariableDef);
        ast.setType(TokenTypes.ParameterDef);
        assertFalse("Should return false when ast type is invalid for current tag", JavadocTagInfo.Serial.isValidOn(ast));
    }
}
