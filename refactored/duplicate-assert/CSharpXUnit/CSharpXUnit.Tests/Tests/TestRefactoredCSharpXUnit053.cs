using Xunit;
using System.Threading;

public class TestInstrumentation
{
    [Fact]
    public void TestCron1()
    {
        var cron = new Instrumentation.Cron();
        long start = System.currentTimeMillis();
        Assert.Equal(0, cron.getStart(), TOLERANCE);
        Assert.Equal(0, cron.getEnd(), TOLERANCE);
        Assert.Equal(cron.getStart(), cron.getEnd());
        Assert.Equal(0, cron.getOwn());
        Assert.Equal(0, cron.getTotal());

        cron.start();
        long s = System.currentTimeMillis();
        Thread.Sleep(INTERVAL);
        cron.stop();
        long realOwnDelay = System.currentTimeMillis() - s;
        long now = System.currentTimeMillis();
        Assert.Equal(start, cron.getStart(), TOLERANCE);
        Assert.Equal(now, cron.getEnd(), TOLERANCE);
        Assert.Equal(INTERVAL, cron.getTotal(), TOLERANCE);
        Assert.Equal(INTERVAL, cron.getOwn(), TOLERANCE);
        Assert.Equal(cron.getTotal(), cron.getOwn(), TOLERANCE);

        long realTotalDelay = System.currentTimeMillis() - s;
        s = System.currentTimeMillis();
        Thread.Sleep(INTERVAL);

        cron.start();

        realTotalDelay += System.currentTimeMillis() - s;

        s = System.currentTimeMillis();
        Thread.Sleep(INTERVAL);
        cron.stop();
        now = System.currentTimeMillis();

        realTotalDelay += System.currentTimeMillis() - s;
        realOwnDelay += System.currentTimeMillis() - s;

        Assert.Equal(realTotalDelay, cron.getTotal(), TOLERANCE);
        Assert.Equal(realOwnDelay, cron.getOwn(), TOLERANCE);
    }

    [Fact]
    public void TestCron2()
    {
        var cron = new Instrumentation.Cron();
        long start = System.currentTimeMillis();
        Assert.Equal(0, cron.getStart(), TOLERANCE);
        Assert.Equal(0, cron.getEnd(), TOLERANCE);
        Assert.Equal(cron.getStart(), cron.getEnd());
        Assert.Equal(0, cron.getOwn());
        Assert.Equal(0, cron.getTotal());

        cron.start();
        long s = System.currentTimeMillis();
        Thread.Sleep(INTERVAL);
        cron.stop();
        long realOwnDelay = System.currentTimeMillis() - s;
        long now = System.currentTimeMillis();
        Assert.Equal(INTERVAL, cron.getTotal(), TOLERANCE);
        Assert.Equal(INTERVAL, cron.getOwn(), TOLERANCE);
        Assert.Equal(cron.getTotal(), cron.getOwn(), TOLERANCE);

        long realTotalDelay = System.currentTimeMillis() - s;
        s = System.currentTimeMillis();
        Thread.Sleep(INTERVAL);

        cron.start();

        realTotalDelay += System.currentTimeMillis() - s;

        s = System.currentTimeMillis();
        Thread.Sleep(INTERVAL);
        cron.stop();
        now = System.currentTimeMillis();

        realTotalDelay += System.currentTimeMillis() - s;
        realOwnDelay += System.currentTimeMillis() - s;

        Assert.Equal(start, cron.getStart(), TOLERANCE);
        Assert.Equal(now, cron.getEnd(), TOLERANCE);
        Assert.Equal(realTotalDelay, cron.getTotal(), TOLERANCE);
        Assert.Equal(realOwnDelay, cron.getOwn(), TOLERANCE);
    }
}
