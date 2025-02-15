public class TestMemoryLocks
{
    [Fact]
    public async Task TestTimeoutTimingOutWriteLock()
    {
        StringBuilder sb = new StringBuilder("");
        Locker l1 = new WriteLocker("a", 1, 0, sb);
        Locker l2 = new WriteLocker("a", 2, 50, sb);

        var t1 = Task.Run(() => l1.Start());
        await Task.Delay(500);
        var t2 = Task.Run(() => l2.Start());
        await Task.Delay(500);
        l1.Finish();
        await Task.Delay(500);
        l2.Finish();
        await Task.Delay(500);
        Assert.Equal("a:1-L a:2-N a:1-U", sb.ToString().Trim());
    }
}
