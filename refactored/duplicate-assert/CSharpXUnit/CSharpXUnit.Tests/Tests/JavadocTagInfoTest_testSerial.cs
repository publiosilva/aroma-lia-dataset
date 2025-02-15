using Xunit;

public class JavadocTagInfoTest {

    [Fact]
    public void TestSerial1() {
        var ast = new DetailAstImpl();
        var astParent = new DetailAstImpl();
        astParent.SetType(TokenTypes.LiteralCatch);
        var setParent = ast.GetType().GetMethod("SetParent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        setParent.Invoke(ast, new object[] { astParent });

        TokenTypes[] validTypes = new TokenTypes[] { TokenTypes.VariableDef };
        foreach (var type in validTypes) {
            ast.SetType(type);
            Assert.True(JavadocTagInfo.Serial.IsValidOn(ast), "Invalid ast type for current tag: " + ast.GetType());
        }

        astParent.SetType(TokenTypes.Slist);
        ast.SetType(TokenTypes.VariableDef);
        Assert.False(JavadocTagInfo.Serial.IsValidOn(ast), "Should return false when ast type is invalid for current tag");
    }

    [Fact]
    public void TestSerial2() {
        var ast = new DetailAstImpl();
        var astParent = new DetailAstImpl();
        astParent.SetType(TokenTypes.LiteralCatch);
        var setParent = ast.GetType().GetMethod("SetParent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        setParent.Invoke(ast, new object[] { astParent });

        TokenTypes[] validTypes = new TokenTypes[] { TokenTypes.VariableDef };
        foreach (var type in validTypes) {
            ast.SetType(type);
            Assert.True(JavadocTagInfo.Serial.IsValidOn(ast), "Invalid ast type for current tag: " + ast.GetType());
        }

        astParent.SetType(TokenTypes.Slist);
        ast.SetType(TokenTypes.VariableDef);
        ast.SetType(TokenTypes.ParameterDef);
        Assert.False(JavadocTagInfo.Serial.IsValidOn(ast), "Should return false when ast type is invalid for current tag");
    }
}
