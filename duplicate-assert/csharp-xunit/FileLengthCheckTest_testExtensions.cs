using Xunit;

public class FileLengthCheckTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestExtensions()
    {
        var check = new FileLengthCheck();
        check.SetFileExtensions("java");
        Assert.Equal(".java", check.GetFileExtensions()[0], "extension should be the same");
        check.SetFileExtensions(".java");
        Assert.Equal(".java", check.GetFileExtensions()[0], "extension should be the same");

        var exception = Assert.Throws<ArgumentException>(() => check.SetFileExtensions(null));
        Assert.Equal("Extensions array can not be null", exception.Message, "Invalid exception message");
    }
}
