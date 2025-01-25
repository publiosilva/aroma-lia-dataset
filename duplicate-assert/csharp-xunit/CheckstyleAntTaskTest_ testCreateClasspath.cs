using Xunit;

public class CheckstyleAntTaskTest : AbstractPathTestSupport
{
    [Fact]
    public void TestCreateClasspath()
    {
        var antTask = new CheckstyleAntTask();

        Assert.Equal("", antTask.CreateClasspath().ToString(), "Invalid classpath");

        antTask.SetClasspath(new Path(new Project(), "/path"));

        Assert.Equal("", antTask.CreateClasspath().ToString(), "Invalid classpath");
    }
}
