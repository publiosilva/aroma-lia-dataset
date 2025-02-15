using Xunit;

public class ArgumentTests
{
    [Fact]
    public void TimeSpanArgumentHasDefault()
    {
        {
            var arg = new SampleTimeSpanArgument(TimeSpan.FromMinutes(3));
            var command = UnitTestCommand.FromArgument(arg);
            int exitCode = command.Invoke(new string[0]);
            Assert.Equal(0, exitCode, "Explanation message");
            Assert.True(command.IsCommandRun(), "Explanation message");
            Assert.Equal(TimeSpan.FromMinutes(3), arg.GetValue(), "Explanation message");
        }
    }
}
