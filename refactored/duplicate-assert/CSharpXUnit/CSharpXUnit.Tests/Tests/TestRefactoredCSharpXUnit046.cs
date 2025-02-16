public class TestCoordELFunctions
{
    [Fact]
    public void TestHours1()
    {
        init("coord-job-submit-freq");
        string expr = "${coord:hours(1)}";
        Assert.Equal("60", CoordELFunctions.evalAndWrap(eval, expr));
        Assert.Equal(TimeUnit.MINUTE, (TimeUnit)eval.getVariable("timeunit"));
    }

    [Fact]
    public void TestHours2()
    {
        init("coord-job-submit-freq");
        string expr = "${coord:hours(coord:hours(1))}";
        Assert.Equal("3600", CoordELFunctions.evalAndWrap(eval, expr));
        Assert.Equal(TimeUnit.MINUTE, (TimeUnit)eval.getVariable("timeunit"));
    }
}
