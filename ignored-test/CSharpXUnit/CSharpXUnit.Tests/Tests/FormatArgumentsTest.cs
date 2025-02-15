using Xunit;

namespace DefaultNamespace
{
    public class StringUtilsTests
    {
        [Fact(Skip = "")]
        public void FormatArgumentsTest()
        {
            {
                var p = new Process();
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "/bin/echo";
                var complexInput = "'";
                p.StartInfo.Arguments = StringUtils.FormatArguments("-n", "foo", complexInput, "bar");
                p.Start();
                var output = p.StandardOutput.ReadToEnd();
                Assert.Equal($"foo {complexInput} bar", output);
            }
        }
    }
}
