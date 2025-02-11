using Xunit;

public class PkgImportRuleTest
{
    [Fact]
    public void TestPkgImportRule()
    {
        var rule = new PkgImportRule(true, false, "pkg", false, false);
        Assert.NotNull(rule, "Rule must not be null");
        Assert.Equal(AccessResult.Unknown, rule.VerifyImport("asda"), "Invalid access result");
        Assert.Equal(AccessResult.Unknown, rule.VerifyImport("p"), "Invalid access result");
        Assert.Equal(AccessResult.Unknown, rule.VerifyImport("pkga"), "Invalid access result");
        Assert.Equal(AccessResult.Allowed, rule.VerifyImport("pkg.a"), "Invalid access result");
        Assert.Equal(AccessResult.Allowed, rule.VerifyImport("pkg.a.b"), "Invalid access result");
        Assert.Equal(AccessResult.Unknown, rule.VerifyImport("pkg"), "Invalid access result");
    }
}
