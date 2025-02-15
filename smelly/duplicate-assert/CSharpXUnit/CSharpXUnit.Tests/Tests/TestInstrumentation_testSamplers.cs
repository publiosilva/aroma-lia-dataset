public class TestInstrumentation
{
    [Fact]
    public void TestCron()
    {
        var cron = new Instrumentation.Cron();
        long start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Assert.Equal(0, cron.GetStart(), TOLERANCE);
        Assert.Equal(0, cron.GetEnd(), TOLERANCE);
        Assert.Equal(cron.GetStart(), cron.GetEnd());
        Assert.Equal(0, cron.GetOwn());
        Assert.Equal(0, cron.GetTotal());

        cron.Start();
        long s = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Thread.Sleep(INTERVAL);
        cron.Stop();
        long realOwnDelay = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - s;
        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Assert.Equal(start, cron.GetStart(), TOLERANCE);
        Assert.Equal(now, cron.GetEnd(), TOLERANCE);
        Assert.Equal(INTERVAL, cron.GetTotal(), TOLERANCE);
        Assert.Equal(INTERVAL, cron.GetOwn(), TOLERANCE);
        Assert.Equal(cron.GetTotal(), cron.GetOwn(), TOLERANCE);

        long realTotalDelay = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - s;
        s = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Thread.Sleep(INTERVAL);

        cron.Start();

        realTotalDelay += DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - s;

        s = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Thread.Sleep(INTERVAL);
        cron.Stop();
        now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        realTotalDelay += DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - s;
        realOwnDelay += DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - s;

        Assert.Equal(start, cron.GetStart(), TOLERANCE);
        Assert.Equal(now, cron.GetEnd(), TOLERANCE);
        Assert.Equal(realTotalDelay, cron.GetTotal(), TOLERANCE);
        Assert.Equal(realOwnDelay, cron.GetOwn(), TOLERANCE);
    }
}
