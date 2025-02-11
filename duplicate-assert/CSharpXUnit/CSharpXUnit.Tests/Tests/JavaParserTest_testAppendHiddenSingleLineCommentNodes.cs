using Xunit;

public class JavaParserTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestAppendHiddenSingleLineCommentNodes()
    {
        var root = JavaParser.ParseFile(new File(GetPath("InputJavaParserHiddenComments.java")), JavaParser.Options.WithComments);

        var singleLineComment = TestUtil.FindTokenInAstByPredicate(root, ast => ast.Type == TokenTypes.SingleLineComment);
        Assert.True(singleLineComment.HasValue, "Single line comment should be present");

        var comment = singleLineComment.Value;

        Assert.Equal(13, comment.LineNo, "Unexpected line number");
        Assert.Equal(0, comment.ColumnNo, "Unexpected column number");
        Assert.Equal("//", comment.Text, "Unexpected comment content");

        var commentContent = comment.FirstChild;

        Assert.Equal(TokenTypes.CommentContent, commentContent.Type, "Unexpected token type");
        Assert.Equal(13, commentContent.LineNo, "Unexpected line number");
        Assert.Equal(2, commentContent.ColumnNo, "Unexpected column number");
        Assert.True(commentContent.Text.StartsWith(" inline comment"), "Unexpected comment content");
    }
}
