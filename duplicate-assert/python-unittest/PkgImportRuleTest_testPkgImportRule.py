import unittest

class PkgImportRuleTest(unittest.TestCase):

    def test_pkg_import_rule(self):
        rule = PkgImportRule(True, False, "pkg", False, False)
        self.assertIsNotNone(rule, "Rule must not be null")
        self.assertEqual(AccessResult.UNKNOWN, rule.verify_import("asda"), "Invalid access result")
        self.assertEqual(AccessResult.UNKNOWN, rule.verify_import("p"), "Invalid access result")
        self.assertEqual(AccessResult.UNKNOWN, rule.verify_import("pkga"), "Invalid access result")
        self.assertEqual(AccessResult.ALLOWED, rule.verify_import("pkg.a"), "Invalid access result")
        self.assertEqual(AccessResult.ALLOWED, rule.verify_import("pkg.a.b"), "Invalid access result")
        self.assertEqual(AccessResult.UNKNOWN, rule.verify_import("pkg"), "Invalid access result")
