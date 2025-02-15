using Xunit;

namespace DefaultNamespace
{
    public class ArgumentTests
    {
        [Fact]
        public void TimeSpanArgumentHasDefault()
        {
            {
                var arg = new SampleTimeSpanArgument(TimeSpan.FromMinutes(3));
                var command = UnitTestCommand.FromArgument(arg);
                var exitCode = command.Invoke(Array.Empty<string>());
                Assert.Equal(0, exitCode);
                Assert.True(command.CommandRun);
                Assert.Equal(TimeSpan.FromMinutes(3), arg.Value);
            }
        }
    }
}
