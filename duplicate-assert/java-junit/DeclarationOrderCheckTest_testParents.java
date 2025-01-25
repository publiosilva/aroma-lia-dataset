import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;

import org.junit.jupiter.api.Test;

public class DeclarationOrderCheckTest extends AbstractModuleTestSupport {

    @Test
    public void testParents() {
        final DetailAstImpl parent = new DetailAstImpl();
        parent.setType(TokenTypes.STATIC_INIT);
        final DetailAstImpl method = new DetailAstImpl();
        method.setType(TokenTypes.METHOD_DEF);
        parent.setFirstChild(method);
        final DetailAstImpl ctor = new DetailAstImpl();
        ctor.setType(TokenTypes.CTOR_DEF);
        method.setNextSibling(ctor);

        final DeclarationOrderCheck check = new DeclarationOrderCheck();

        check.visitToken(method);
        final SortedSet<LocalizedMessage> messages1 = check.getMessages();

        assertEquals(0, messages1.size(), "No exception messages expected");

        check.visitToken(ctor);
        final SortedSet<LocalizedMessage> messages2 = check.getMessages();

        assertEquals(0, messages2.size(), "No exception messages expected");
    }
}
