public class TestShell
{
    [Fact]
    public void TestShellCommandTimerLeak()
    {
        string[] quickCommand = new string[] { "/bin/sleep", "100" };

        int timersBefore = CountTimerThreads();
        Console.Error.WriteLine("before: " + timersBefore);

        for (int i = 0; i < 10; i++)
        {
            var shexec = new Shell.ShellCommandExecutor(quickCommand, null, null, 1);
            Exception exception = Record.Exception(() => shexec.Execute());
            Assert.NotNull(exception);
        }
        
        Thread.Sleep(1000);
        int timersAfter = CountTimerThreads();
        Console.Error.WriteLine("after: " + timersAfter);
        Assert.Equal(timersBefore, timersAfter);
    }
}
