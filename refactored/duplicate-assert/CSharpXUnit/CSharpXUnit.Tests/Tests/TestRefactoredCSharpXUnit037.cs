using Xunit;

public class FileLengthCheckTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestExtensions1()
    {
        var check = new FileLengthCheck();
        check.SetFileExtensions("java");
        Assert.Equal(".java", check.GetFileExtensions()[0]);

        var exception = Assert.Throws<ArgumentException>(() => check.SetFileExtensions(null));
        Assert.Equal("Extensions array can not be null", exception.Message);
    }

    [Fact]
    public void TestExtensions2()
    {
        var check = new FileLengthCheck();
        check.SetFileExtensions(".java");
        Assert.Equal(".java", check.GetFileExtensions()[0]);

        var exception = Assert.Throws<ArgumentException>(() => check.SetFileExtensions(null));
        Assert.Equal("Extensions array can not be null", exception.Message);
    }
}
