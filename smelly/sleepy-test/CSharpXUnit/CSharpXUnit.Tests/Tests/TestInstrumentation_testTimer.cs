public class TestInstrumentation
{
    [Fact]
    public void TestTimer()
    {
        var timer = new Instrumentation.Timer();

        Assert.Equal(0, timer.GetTicks());
        Assert.Equal(0, timer.GetTotal());
        Assert.Equal(0, timer.GetOwn());
        Assert.Equal(0, timer.GetOwnAvg());
        Assert.Equal(0, timer.GetTotalAvg());
        Assert.Equal(0, timer.GetOwnSquareSum());
        Assert.Equal(0, timer.GetTotalSquareSum());
        Assert.Equal(0, timer.GetOwnMin());
        Assert.Equal(0, timer.GetOwnMax());
        Assert.Equal(0, timer.GetTotalMin());
        Assert.Equal(0, timer.GetTotalMax());

        Assert.Equal(0, timer.GetValue().GetTicks());
        Assert.Equal(0, timer.GetValue().GetTotal());
        Assert.Equal(0, timer.GetValue().GetOwn());
        Assert.Equal(0, timer.GetValue().GetOwnAvg());
        Assert.Equal(0, timer.GetValue().GetTotalAvg());
        Assert.Equal(0, timer.GetValue().GetOwnSquareSum());
        Assert.Equal(0, timer.GetValue().GetTotalSquareSum());
        Assert.Equal(0, timer.GetValue().GetOwnMin());
        Assert.Equal(0, timer.GetValue().GetOwnMax());
        Assert.Equal(0, timer.GetValue().GetTotalMin());
        Assert.Equal(0, timer.GetValue().GetTotalMax());

        var cron1 = new Instrumentation.Cron();
        cron1.Start();
        Thread.Sleep(INTERVAL);
        cron1.Stop();
        timer.AddCron(cron1);

        Assert.Equal(1, timer.GetTicks());
        Assert.Equal(cron1.GetTotal(), timer.GetTotal());
        Assert.Equal(cron1.GetOwn(), timer.GetOwn());
        Assert.Equal(cron1.GetOwn(), timer.GetOwnAvg());
        Assert.Equal(cron1.GetTotal(), timer.GetTotalAvg());
        Assert.Equal(cron1.GetOwn() * cron1.GetOwn(), timer.GetOwnSquareSum());
        Assert.Equal(cron1.GetTotal() * cron1.GetTotal(), timer.GetTotalSquareSum());
        Assert.Equal(cron1.GetOwn(), timer.GetOwnMin());
        Assert.Equal(cron1.GetOwn(), timer.GetOwnMax());
        Assert.Equal(cron1.GetTotal(), timer.GetTotalMin());
        Assert.Equal(cron1.GetTotal(), timer.GetTotalMax());

        Assert.Equal(1, timer.GetValue().GetTicks());
        Assert.Equal(cron1.GetTotal(), timer.GetValue().GetTotal());
        Assert.Equal(cron1.GetOwn(), timer.GetValue().GetOwn());
        Assert.Equal(cron1.GetOwn(), timer.GetValue().GetOwnAvg());
        Assert.Equal(cron1.GetTotal(), timer.GetValue().GetTotalAvg());
        Assert.Equal(cron1.GetOwn() * cron1.GetOwn(), timer.GetValue().GetOwnSquareSum());
        Assert.Equal(cron1.GetTotal() * cron1.GetTotal(), timer.GetValue().GetTotalSquareSum());
        Assert.Equal(cron1.GetOwn(), timer.GetValue().GetOwnMin());
        Assert.Equal(cron1.GetOwn(), timer.GetValue().GetOwnMax());
        Assert.Equal(cron1.GetTotal(), timer.GetValue().GetTotalMin());
        Assert.Equal(cron1.GetTotal(), timer.GetValue().GetTotalMax());

        var cron2 = new Instrumentation.Cron();
        cron2.Start();
        Thread.Sleep(INTERVAL * 2);
        cron2.Stop();
        timer.AddCron(cron2);

        Assert.Equal(2, timer.GetTicks());
        Assert.Equal(cron1.GetTotal() + cron2.GetTotal(), timer.GetTotal());
        Assert.Equal(cron1.GetOwn() + cron2.GetOwn(), timer.GetOwn());
        Assert.Equal((cron1.GetOwn() + cron2.GetOwn()) / 2, timer.GetOwnAvg());
        Assert.Equal((cron1.GetTotal() + cron2.GetTotal()) / 2, timer.GetTotalAvg());
        Assert.Equal(cron1.GetOwn() * cron1.GetOwn() + cron2.GetOwn() * cron2.GetOwn(), timer.GetOwnSquareSum());
        Assert.Equal(cron1.GetTotal() * cron1.GetTotal() + cron2.GetTotal() * cron2.GetTotal(), timer.GetTotalSquareSum());
        Assert.Equal(cron1.GetOwn(), timer.GetOwnMin());
        Assert.Equal(cron2.GetOwn(), timer.GetOwnMax());
        Assert.Equal(cron1.GetTotal(), timer.GetTotalMin());
        Assert.Equal(cron2.GetTotal(), timer.GetTotalMax());
    }
}
