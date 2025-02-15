import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class JavadocTagInfoTest {

    @Test
    public void testSerial() {
        DetailAstImpl ast = new DetailAstImpl();
        DetailAstImpl astParent = new DetailAstImpl();
        astParent.setType(TokenTypes.LiteralCatch);
        var setParent = ast.getClass().getDeclaredMethod("setParent", DetailAstImpl.class);
        setParent.setAccessible(true);
        setParent.invoke(ast, astParent);

        TokenTypes[] validTypes = new TokenTypes[]{TokenTypes.VariableDef};
        for (TokenTypes type : validTypes) {
            ast.setType(type);
            assertTrue(JavadocTagInfo.Serial.isValidOn(ast), "Invalid ast type for current tag: " + ast.getClass());
        }

        astParent.setType(TokenTypes.Slist);
        ast.setType(TokenTypes.VariableDef);
        assertFalse(JavadocTagInfo.Serial.isValidOn(ast), "Should return false when ast type is invalid for current tag");

        ast.setType(TokenTypes.ParameterDef);
        assertFalse(JavadocTagInfo.Serial.isValidOn(ast), "Should return false when ast type is invalid for current tag");
    }
}
