import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotEquals;

import org.junit.jupiter.api.Test;

public class XpathUtilTest {

    @Test
    public void testGetValue() {
        assertEquals("HELLO WORLD", getTextAttributeValue(
                createDetailAST(TokenTypes.STRING_LITERAL, "\"HELLO WORLD\"")),
                "Returned value differs from expected");
        assertEquals("123", getTextAttributeValue(createDetailAST(TokenTypes.NUM_INT, "123")),
                "Returned value differs from expected");
        assertEquals("HELLO WORLD",
                getTextAttributeValue(createDetailAST(TokenTypes.IDENT, "HELLO WORLD")),
                "Returned value differs from expected");
        assertNotEquals("HELLO WORLD",
                getTextAttributeValue(createDetailAST(TokenTypes.STRING_LITERAL, "HELLO WORLD")),
                "Returned value differs from expected");
    }
}
