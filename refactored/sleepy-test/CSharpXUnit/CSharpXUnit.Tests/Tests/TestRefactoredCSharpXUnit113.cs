public class TestMemoryLocks
{
    [Fact]
    public void TestTimeoutTimingOutWriteLock()
    {
        StringBuilder sb = new StringBuilder("");
        Locker l1 = new WriteLocker("a", 1, 0, sb);
        Locker l2 = new WriteLocker("a", 2, 50, sb);

        new Thread(l1.Run).Start();
        // Thread.Sleep(500);
        new Thread(l2.Run).Start();
        // Thread.Sleep(500);
        l1.Finish();
        // Thread.Sleep(500);
        l2.Finish();
        // Thread.Sleep(500);
        Assert.Equal("a:1-L a:2-N a:1-U", sb.ToString().Trim());
    }
}