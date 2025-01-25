using Xunit;

public class XpathUtilTest
{
    [Fact]
    public void TestGetValue()
    {
        Assert.Equal("HELLO WORLD", 
            GetTextAttributeValue(CreateDetailAST(TokenTypes.StringLiteral, "\"HELLO WORLD\"")), 
            "Returned value differs from expected");
        
        Assert.Equal("123", 
            GetTextAttributeValue(CreateDetailAST(TokenTypes.NumInt, "123")), 
            "Returned value differs from expected");
        
        Assert.Equal("HELLO WORLD", 
            GetTextAttributeValue(CreateDetailAST(TokenTypes.Ident, "HELLO WORLD")), 
            "Returned value differs from expected");
        
        Assert.NotEqual("HELLO WORLD", 
            GetTextAttributeValue(CreateDetailAST(TokenTypes.StringLiteral, "HELLO WORLD")), 
            "Returned value differs from expected");
    }
}
