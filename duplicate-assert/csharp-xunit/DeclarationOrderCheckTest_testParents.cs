using Xunit;

public class DeclarationOrderCheckTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestParents()
    {
        var parent = new DetailAstImpl();
        parent.SetType(TokenTypes.StaticInit);
        var method = new DetailAstImpl();
        method.SetType(TokenTypes.MethodDef);
        parent.SetFirstChild(method);
        var ctor = new DetailAstImpl();
        ctor.SetType(TokenTypes.CtorDef);
        method.SetNextSibling(ctor);

        var check = new DeclarationOrderCheck();

        check.VisitToken(method);
        var messages1 = check.GetMessages();

        Assert.Equal(0, messages1.Count, "No exception messages expected");

        check.VisitToken(ctor);
        var messages2 = check.GetMessages();

        Assert.Equal(0, messages2.Count, "No exception messages expected");
    }
}
