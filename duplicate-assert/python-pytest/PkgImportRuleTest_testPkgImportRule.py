def test_pkg_import_rule():
    rule = PkgImportRule(True, False, "pkg", False, False)
    assert rule is not None, "Rule must not be null"
    assert rule.verifyImport("asda") == AccessResult.UNKNOWN, "Invalid access result"
    assert rule.verifyImport("p") == AccessResult.UNKNOWN, "Invalid access result"
    assert rule.verifyImport("pkga") == AccessResult.UNKNOWN, "Invalid access result"
    assert rule.verifyImport("pkg.a") == AccessResult.ALLOWED, "Invalid access result"
    assert rule.verifyImport("pkg.a.b") == AccessResult.ALLOWED, "Invalid access result"
    assert rule.verifyImport("pkg") == AccessResult.UNKNOWN, "Invalid access result"
