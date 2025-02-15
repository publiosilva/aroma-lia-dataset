public class TestMemoryLocks
{
    [Fact]
    public void TestReadLock()
    {
        var sb = new StringBuilder("");
        var l1 = new ReadLocker("a", 1, -1, sb);
        var l2 = new ReadLocker("a", 2, -1, sb);

        new Thread(l1.Run).Start();
        // Thread.Sleep(500);
        new Thread(l2.Run).Start();
        // Thread.Sleep(500);
        l1.Finish();
        // Thread.Sleep(500);
        l2.Finish();
        // Thread.Sleep(500);
        Assert.Equal("a:1-L a:2-L a:1-U a:2-U", sb.ToString().Trim());
    }
}
