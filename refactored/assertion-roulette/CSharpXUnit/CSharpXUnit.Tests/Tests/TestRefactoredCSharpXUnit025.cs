using Xunit;

namespace DefaultNamespace
{
    public class ArgumentTests
    {
        [Fact]
        public void TimeSpanArgumentHasDefault1()
        {
            {
                var arg = new SampleTimeSpanArgument(TimeSpan.FromMinutes(3));
                var command = UnitTestCommand.FromArgument(arg);
                var exitCode = command.Invoke(Array.Empty<string>());
                Assert.Equal(0, exitCode);
            }
        }

        [Fact]
        public void TimeSpanArgumentHasDefault2()
        {
            {
                var arg = new SampleTimeSpanArgument(TimeSpan.FromMinutes(3));
                var command = UnitTestCommand.FromArgument(arg);
                var exitCode = command.Invoke(Array.Empty<string>());
                Assert.True(command.CommandRun);
            }
        }

        [Fact]
        public void TimeSpanArgumentHasDefault3()
        {
            {
                var arg = new SampleTimeSpanArgument(TimeSpan.FromMinutes(3));
                var command = UnitTestCommand.FromArgument(arg);
                var exitCode = command.Invoke(Array.Empty<string>());
                Assert.Equal(TimeSpan.FromMinutes(3), arg.Value);
            }
        }
    }
}
