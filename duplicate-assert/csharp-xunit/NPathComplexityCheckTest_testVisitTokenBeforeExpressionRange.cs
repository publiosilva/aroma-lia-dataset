using Xunit;

public class NPathComplexityCheckTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestVisitTokenBeforeExpressionRange()
    {
        var astIf = MockAST(TokenTypes.LiteralIf, "if", "mockfile", 2, 2);
        var astIfLeftParen = MockAST(TokenTypes.LParen, "(", "mockfile", 3, 3);
        astIf.AddChild(astIfLeftParen);
        var astIfTrue = MockAST(TokenTypes.LiteralTrue, "true", "mockfile", 3, 3);
        astIf.AddChild(astIfTrue);
        var astIfRightParen = MockAST(TokenTypes.RParen, ")", "mockfile", 4, 4);
        astIf.AddChild(astIfRightParen);

        var astTernary = MockAST(TokenTypes.Question, "?", "mockfile", 1, 1);
        var astTernaryTrue = MockAST(TokenTypes.LiteralTrue, "true", "mockfile", 1, 2);
        astTernary.AddChild(astTernaryTrue);

        var npathComplexityCheckObj = new NPathComplexityCheck();

        npathComplexityCheckObj.VisitToken(astIf);
        var messages1 = npathComplexityCheckObj.GetMessages();

        Assert.Equal(0, messages1.Count, "No exception messages expected");

        npathComplexityCheckObj.VisitToken(astTernary);
        var messages2 = npathComplexityCheckObj.GetMessages();

        Assert.Equal(0, messages2.Count, "No exception messages expected");
    }
}
