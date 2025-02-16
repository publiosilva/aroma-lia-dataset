using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

public class StringUtilsTests {
    
    [Fact]
    // [Fact(Skip = "Skipping test")]
    public void formatArgumentsTest() {
        var p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.FileName = "/bin/echo";
        p.StartInfo.Arguments = "-n foo ' bar";

        p.Start();
        string output = new StreamReader(p.StandardOutput.BaseStream).ReadToEnd();
        p.WaitForExit();

        Assert.Equal("foo ' bar", output);
    }
}
