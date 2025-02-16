using Xunit;

public class CheckstyleAntTaskTest : AbstractPathTestSupport
{
    [Fact]
    public void TestCreateClasspath1()
    {
        CheckstyleAntTask antTask = new CheckstyleAntTask();

        Assert.Equal("", antTask.CreateClasspath().ToString(), "Invalid classpath");
    }

    [Fact]
    public void TestCreateClasspath2()
    {
        CheckstyleAntTask antTask = new CheckstyleAntTask();

        antTask.SetClasspath(new Path(new Project(), "/path"));

        Assert.Equal("", antTask.CreateClasspath().ToString(), "Invalid classpath");
    }
}
