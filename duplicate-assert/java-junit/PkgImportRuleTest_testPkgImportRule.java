import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;

import org.junit.jupiter.api.Test;

public class PkgImportRuleTest {

    @Test
    public void testPkgImportRule() {
        final PkgImportRule rule = new PkgImportRule(true, false, "pkg", false, false);
        assertNotNull(rule, "Rule must not be null");
        assertEquals(AccessResult.UNKNOWN, rule.verifyImport("asda"), "Invalid access result");
        assertEquals(AccessResult.UNKNOWN, rule.verifyImport("p"), "Invalid access result");
        assertEquals(AccessResult.UNKNOWN, rule.verifyImport("pkga"), "Invalid access result");
        assertEquals(AccessResult.ALLOWED, rule.verifyImport("pkg.a"), "Invalid access result");
        assertEquals(AccessResult.ALLOWED, rule.verifyImport("pkg.a.b"), "Invalid access result");
        assertEquals(AccessResult.UNKNOWN, rule.verifyImport("pkg"), "Invalid access result");
    }
}
