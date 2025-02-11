using Xunit;

public class JavaParserTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestAppendHiddenBlockCommentNodes()
    {
        var root = JavaParser.ParseFile(new File(GetPath("InputJavaParserHiddenComments.java")), JavaParser.Options.WithComments);

        var blockComment = TestUtil.FindTokenInAstByPredicate(root, ast => ast.Type == TokenTypes.BlockCommentBegin);

        Assert.True(blockComment.HasValue, "Block comment should be present");

        var comment = blockComment.Value;

        Assert.Equal(3, comment.LineNo, "Unexpected line number");
        Assert.Equal(0, comment.ColumnNo, "Unexpected column number");
        Assert.Equal("/*", comment.Text, "Unexpected comment content");

        var commentContent = comment.FirstChild;
        var commentEnd = comment.LastChild;

        Assert.Equal(3, commentContent.LineNo, "Unexpected line number");
        Assert.Equal(2, commentContent.ColumnNo, "Unexpected column number");
        Assert.Equal(9, commentEnd.LineNo, "Unexpected line number");
        Assert.Equal(1, commentEnd.ColumnNo, "Unexpected column number");
    }
}
