public class TestMemoryLocks
{
    [Fact]
    public void TestWaitWriteLock()
    {
        StringBuilder sb = new StringBuilder("");
        Locker l1 = new WriteLocker("a", 1, -1, sb);
        Locker l2 = new WriteLocker("a", 2, -1, sb);

        new Thread(l1).Start();
        // Thread.Sleep(500);
        new Thread(l2).Start();
        // Thread.Sleep(500);
        l1.Finish();
        // Thread.Sleep(500);
        l2.Finish();
        // Thread.Sleep(500);
        Assert.Equal("a:1-L a:1-U a:2-L a:2-U", sb.ToString().Trim());
    }
}
